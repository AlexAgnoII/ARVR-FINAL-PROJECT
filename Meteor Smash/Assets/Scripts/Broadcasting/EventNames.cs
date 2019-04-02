using UnityEngine;
using System.Collections;

/*
 * Holder for event names
 * Created By: NeilDG
 */ 
public class EventNames {
	public const string ON_UPDATE_SCORE = "ON_UPDATE_SCORE";
	public const string ON_CORRECT_MATCH = "ON_CORRECT_MATCH";
	public const string ON_WRONG_MATCH = "ON_WRONG_MATCH";
	public const string ON_INCREASE_LEVEL = "ON_INCREASE_LEVEL";

	public const string ON_PICTURE_CLICKED = "ON_PICTURE_CLICKED";

    public class MeteorSmash
    {
        public const string ON_GAME_WON = "ON_GAME_WON"; //Called on gamethrowhandler when game is successful, observed by GameThrowScreenScript.
        
        //called on meteorscript, observed on GameThrowHandlerScript
        public const string ON_METEOR_HIT_TARGET = "ON_METEOR_HIT_TARGET"; //event name called when ball thrown hits target called on meteorscsript
        public const string ON_METEOR_HIT_NOTHING = "ON_METEOR_HIT_NOTHING";// "" ball thrown did not hit target called on meteor script

        //test event name to check for target position when spawned. called on placementhandler script, Observerd by GameManagerScript.
        public const string ON_SPAWN_TARGET_DONE = "ON_SPAWN_TARGET_DONE";
        public const string VALUE_TARGET_POSITION_X = "VALUE_TARGET_POSITION_X";
        public const string VALUE_TARGET_POSITION_Y = "VALUE_TARGET_POSITION_Y";
        public const string VALUE_TARGET_POSITION_Z = "VALUE_TARGET_POSITION_Z";

        //this on though is called on gamemanager sscript, Observed by GameThrowScreensscript.
        public const string ON_PRINT_TARGET_POSITION = "ON_PRINT_TARGET_POSITION"; 
        public const string VALUE_TARGET_COORDINATES = "VALUE_TARGET_COORDINATES";

        //Posted on gamemanager, observed by gamethrowscreen.
        public const string ON_PLAYER_VALID_DISTANCE = "ON_PLAYER_VALID_DISTANCE";
        public const string ON_PLAYER_INVALID_DISTANCE = "ON_PLAYER_INVALID_DISTANCE";

    }




	public class ARBluetoothEvents {
		public const string ON_START_BLUETOOTH_DEMO = "ON_START_BLUETOOTH_DEMO";
		public const string ON_RECEIVED_MESSAGE = "ON_RECEIVED_MESSAGE";
	}

	public class ARPhysicsEvents {
		public const string ON_FIRST_TARGET_SCAN = "ON_FIRST_TARGET_SCAN";
		public const string ON_FINAL_TARGET_SCAN = "ON_FINAL_TARGET_SCAN";
	}

	public class ExtendTrackEvents {
		public const string ON_TARGET_SCAN = "ON_TARGET_SCAN";
		public const string ON_TARGET_HIDE = "ON_TARGET_HIDE";
		public const string ON_SHOW_ALL = "ON_SHOW_ALL";
		public const string ON_HIDE_ALL = "ON_HIDE_ALL";
		public const string ON_DELETE_ALL = "ON_DELETE_ALL";
	}

	public class X01_Events {
		public const string ON_FIRST_SCAN = "ON_FIRST_SCAN";
		public const string ON_FINAL_SCAN = "ON_FINAL_SCAN";
		public const string EXTENDED_TRACK_ON_SCAN = "EXTENDED_TRACK_ON_SCAN";
		public const string EXTENDED_TRACK_REMOVED = "EXTENDED_TRACK_REMOVED";
	}

	public class X22_Events {
		public const string ON_FIRST_SCAN = "ON_FIRST_SCAN";
		public const string ON_FINAL_SCAN = "ON_FINAL_SCAN";
		public const string EXTENDED_TRACK_ON_SCAN = "EXTENDED_TRACK_ON_SCAN";
		public const string EXTENDED_TRACK_REMOVED = "EXTENDED_TRACK_REMOVED";
	}

	public class S18_Events {
		public const string ON_FIRST_SCAN = "FIRST_TARGET_SCAN";
		public const string ON_FINAL_SCAN = "ON_FINAL_SCAN";
	}
}







