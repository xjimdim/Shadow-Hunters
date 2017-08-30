using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class FBHolder : MonoBehaviour {

	public static FBHolder instance;
	public GameObject UIFBIsLoggedIn;
	public GameObject UIFBIsNotLoggedIn;
	public GameObject UIFBAvatar;
	public GameObject UIFBUserName;

	public GameObject ScoreEntryPanel;
	public GameObject ScoreScrollList;

	public String MyName;
	public String MyID;
	public Image UserAvatar;


	private List<object> scoresList = null;

	//private List<object> userHighScore = null;

	private Dictionary<string, string> profile = null;

	AccessToken aToken;

	void Awake(){
		instance = this;
		DontDestroyOnLoad(transform.gameObject);
		DealWithFBMenus (false);
		FB.Init (SetInit, OnHideUnity);
	}

	private void SetInit(){
		Debug.Log ("FB Init Done");

		if (FB.IsLoggedIn) {
			Debug.Log ("FB Logged in");
			DealWithFBMenus(true);	
		}
		else{
			DealWithFBMenus(false);	
		}
	}

	private void OnHideUnity (bool isGameShown){

		if (!isGameShown) {
			Time.timeScale = 0;			

		} 
		else {
			Time.timeScale = 1;
		}

	}

	public void FBLogin(){
		Debug.Log ("trying to login");
		FB.LogInWithReadPermissions (
			new List<string>(){"public_profile", "email", "user_friends"},
		AuthCallback
		);

		//FB.LogInWithReadPermissions (new List<string>(){"email", "user_friends"}, AuthCallback);
	}

	void AuthCallback(ILoginResult result){
		if (FB.IsLoggedIn) {
			Debug.Log ("FB login worked");	
			aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			NetworkManager.instance.RoomGenerator();
			DealWithFBMenus(true);
		}
		else{
			Debug.Log("FB Login Fail");
			DealWithFBMenus(false);
		}
	}

	void DealWithFBMenus(bool isLoeggedIn){
		if (isLoeggedIn) {
			UIFBIsLoggedIn.SetActive(true);
			UIFBIsNotLoggedIn.SetActive(false);


			//get profile picture
			FB.API (Util.GetPictureURL("me", 128, 128), HttpMethod.GET, DealWithProfilePicture);


			//get user name
			FB.API ("/me?fields=id,first_name", HttpMethod.GET, DealWithUserName);

			//QueryScores();

		} 
		else {
			UIFBIsLoggedIn.SetActive(false);
			UIFBIsNotLoggedIn.SetActive(true);
			Debug.Log ("FB login button appear...");
		}
	}

	void DealWithProfilePicture(IGraphResult result){
		if (result.Error != null) {
			Debug.Log("Problem with getting profile picture");	
			FB.API (Util.GetPictureURL("me", 128, 128),HttpMethod.GET, DealWithProfilePicture);
			return;
		}

		UserAvatar = UIFBAvatar.GetComponent<Image> (); 
		UserAvatar.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 (0, 0));

	}

	void DealWithUserName(IGraphResult result){
		if (result.Error != null) {
			Debug.Log("Problem with user name");	
			FB.API ("/me?fields=id,first_name", HttpMethod.GET, DealWithUserName);
			return;
		}
		profile = Util.DeserializeJSONProfile (result.RawResult);

		Text UserMsg = UIFBUserName.GetComponent<Text> ();


		UserMsg.text = "Hello, " + profile ["first_name"]; 

		MyName = profile ["first_name"].ToString ();
		MyID = profile ["id"].ToString ();
		
	}

	public void ShareWithFriends(){

//		FB.FeedShare (
//			toId: "",
//			link: "http://apps.facebook.com/"+ FB.AppId + "/?challenge_brack=" + (FB.IsLoggedIn ? aToken.UserId : "guest"),
//			linkName: "BEST GAME EVER",
//			linkCaption: "This is the best game I have ever played!!!",
//			linkDescription: "Shadow hunters is probably the best RPG card game in the universe",
//			picture: "https://uberfacts.files.wordpress.com/2013/02/asteroid.jpg",
//			mediaSource: "",
//			callback = null
//			);

		FB.FeedShare(
			toId: aToken.UserId,
			link: new System.Uri("https://apps.facebook.com/theshadowhunters"),
			linkName: "BEST GAME EVER",
			linkCaption: "This is the best game I have ever played!!!",
			linkDescription: "Shadow hunters is probably the best RPG card game in the universe",
			picture: new System.Uri("https://uberfacts.files.wordpress.com/2013/02/asteroid.jp"),
			mediaSource: "",
			callback: null
			);
	}

	public void InviteFriends(){
		
		FB.AppRequest (
			message: "Hey, come play this epic game!",
			title: "Invite your friends to join you!!"
			);
	}

	//Scores handling: 

	public void QueryScores(){
		FB.API ("/app/scores?fields=score,user.limit(40)", HttpMethod.GET, ScoresCallback);
	}

	private void ScoresCallback(IGraphResult result) {
		Debug.Log ("Scores result: " + result.RawResult);

 		
		scoresList = Util.DeserializeScores(result.RawResult);

		foreach (Transform child in ScoreScrollList.transform) {
			GameObject.Destroy(child.gameObject);
		}


		foreach (object score in scoresList) {
			var entry = (Dictionary<string,object>) score;
			var user = (Dictionary<string,object>) entry["user"];


		
			GameObject ScorePanel;
			ScorePanel = Instantiate(ScoreEntryPanel) as GameObject;
			ScorePanel.transform.parent = ScoreScrollList.transform;

			Transform ThisScoreName = ScorePanel.transform.Find("NamePanelBG").Find("FriendName");
			Transform ThisScoreScore = ScorePanel.transform.Find("ScorePanelBG").Find("FriendScore");;

			Text ScoreName = ThisScoreName.GetComponent<Text>();
			Text ScoreScore = ThisScoreScore.GetComponent<Text>(); 


			string temp = user["name"].ToString();
			string temp2 = temp.Substring(0, temp.IndexOf(" ")+1);  //get only the name
			ScoreName.text = temp2.ToString();
			ScoreScore.text = entry["score"].ToString();

			Transform TheUserAvatar = ScorePanel.transform.Find("FriendAvatar");
			Debug.Log ("found: " + TheUserAvatar.name);
			Image UserAvatar = TheUserAvatar.GetComponent<Image>();

			FB.API (Util.GetPictureURL(user["id"].ToString(), 128, 128), HttpMethod.GET, delegate(IGraphResult pictureResult) {
				if(pictureResult.Error != null) { // in case there was an error
					Debug.Log (pictureResult.Error);
				}
				else { //we got the image
					UserAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0, 0));
				}
			});

		}
	
	}
 

	public void PlayGame(){
		DontDestroyOnLoad(gameObject);
		SceneManager.LoadScene("_scene");
	}


}
