/*
 * GSClient.cs
 *
 * authors: Tomasz Rybiarczyk (tomasz.rybiarczyk@steelseries.com)
 *
 *
 * Copyright (c) 2016 SteelSeries
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System.IO;
using UnityEngine;


namespace SteelSeries {

    namespace GameSense {

        public class GSClient : MonoBehaviour {

            // ******************** EDITOR VISIBLE  ********************

            public string GameName;
            public string GameDisplayName;
            public IconColor IconColor;
            public Bind_Event[] Events;




            // ******************** TYPES ********************

            [System.Serializable] struct coreProps {
#pragma warning disable 0649
                public string address;
#pragma warning restore 0649
            }

            enum ClientState {
                Active,
                Probing,
                Inactive
            }

            [FullSerializer.fsObject(Converter = typeof(BindEventConverter))]
            [System.Serializable] public class Bind_Event {

                [System.NonSerialized] public string game;
                public string eventName;
                public System.Int32 minValue;
                public System.Int32 maxValue;
                public EventIconId iconId;
                public AbstractHandler[] handlers;

                public Bind_Event( string gameName, string eventName, System.Int32 minValue, System.Int32 maxValue, EventIconId iconId, AbstractHandler[] handlers) {
                    this.game = gameName;
                    this.eventName = eventName;
                    this.minValue = minValue;
                    this.maxValue = maxValue;
                    this.iconId = iconId;
                    this.handlers = handlers;
                }
            }

            [FullSerializer.fsObject(Converter = typeof(RegisterEventConverter))]
            class Register_Event {

                [System.NonSerialized] public string game;
                public string eventName;
                public System.Int32 minValue;
                public System.Int32 maxValue;
                public EventIconId iconId;

                public Register_Event( string gameName, string eventName, System.Int32 minValue, System.Int32 maxValue, EventIconId iconId ) {
                    this.game = gameName;
                    this.eventName = eventName;
                    this.minValue = minValue;
                    this.maxValue = maxValue;
                    this.iconId = iconId;
                }
            }

            [FullSerializer.fsObject(Converter = typeof(SendEventConverter))]
            class Send_Event {
                public string game;
                public string event_name;
                public EventData data;
            }

            [FullSerializer.fsObject(Converter = typeof(RegisterGameConverter))]
            class Register_Game {
                public string game;
                public string game_display_name;
                public IconColor icon_color_id;
            }

            class Game {
                public string game;
                public Game( string gameName ) {
                    game = gameName;
                }
            }


            class BindEventConverter : FullSerializer.fsDirectConverter< Bind_Event > {
                protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref Bind_Event model ) {
                    return FullSerializer.fsResult.Fail( "Not implemented" );
                }

                protected override FullSerializer.fsResult DoSerialize( Bind_Event model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                    // TODO check result of each
                    SerializeMember< string >( serialized, null, "game", model.game.ToUpper() );
                    SerializeMember< string >( serialized, null, "event", model.eventName );
                    SerializeMember< System.Int32 >( serialized, null, "min_value", model.minValue );
                    SerializeMember< System.Int32 >( serialized, null, "max_value", model.maxValue );
                    SerializeMember< System.UInt32 >( serialized, null, "icon_id", (System.UInt32)model.iconId );
                    SerializeMember< AbstractHandler[] >( serialized, null, "handlers", model.handlers );

                    return FullSerializer.fsResult.Success;
                }
            }

            class RegisterEventConverter : FullSerializer.fsDirectConverter< Register_Event > {
                protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref Register_Event model ) {
                    return FullSerializer.fsResult.Fail( "Not implemented" );
                }

                protected override FullSerializer.fsResult DoSerialize( Register_Event model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                    // TODO check result of each
                    SerializeMember< string >( serialized, null, "game", model.game.ToUpper() );
                    SerializeMember< string >( serialized, null, "event", model.eventName );
                    SerializeMember< System.Int32 >( serialized, null, "min_value", model.minValue );
                    SerializeMember< System.Int32 >( serialized, null, "max_value", model.maxValue );
                    SerializeMember< System.UInt32 >( serialized, null, "icon_id", (System.UInt32)model.iconId );

                    return FullSerializer.fsResult.Success;
                }
            }

            class SendEventConverter : FullSerializer.fsDirectConverter< Send_Event > {
                protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref Send_Event model ) {
                    return FullSerializer.fsResult.Fail( "Not implemented" );
                }

                protected override FullSerializer.fsResult DoSerialize( Send_Event model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                    // TODO check result of each
                    SerializeMember< string >( serialized, null, "game", model.game );
                    SerializeMember< string >( serialized, null, "event", model.event_name );
                    SerializeMember< EventData >( serialized, null, "data", model.data );

                    return FullSerializer.fsResult.Success;
                }
            }

            class RegisterGameConverter : FullSerializer.fsDirectConverter< Register_Game > {
                protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref Register_Game model ) {
                    return FullSerializer.fsResult.Fail( "Not implemented" );
                }

                protected override FullSerializer.fsResult DoSerialize( Register_Game model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                    // TODO check result of each
                    SerializeMember< string >( serialized, null, "game", model.game );
                    SerializeMember< string >( serialized, null, "game_display_name", model.game_display_name );
                    SerializeMember< System.UInt32 >( serialized, null, "icon_color_id", (System.UInt32)model.icon_color_id );

                    return FullSerializer.fsResult.Success;
                }
            }


            abstract class QueueMsg {
                protected object _data;
                public abstract object data { get; set; }
                public abstract System.Uri uri { get; }
                public abstract bool IsCritical();
            }

            class QueueMsgRegisterGame : QueueMsg {
                public static System.Uri _uri;
                public override System.Uri uri { get { return _uri; } }
                public override object data {
                    get { return _data as Register_Game; }
                    set { _data = value; }
                }
                public override bool IsCritical() { return true; }
            }

            class QueueMsgBindEvent : QueueMsg {
                public static System.Uri _uri;
                public override System.Uri uri { get { return _uri; } }
                public override object data {
                    get { return _data as Bind_Event; }
                    set { _data = value; }
                }
                public override bool IsCritical() { return true; }
            }

            class QueueMsgRegisterEvent : QueueMsg {
                public static System.Uri _uri;
                public override System.Uri uri { get { return _uri; } }
                public override object data {
                    get { return _data as Register_Event; }
                    set { _data = value; }
                }
                public override bool IsCritical() { return true; }
            }

            class QueueMsgSendEvent : QueueMsg {
                public static System.Uri _uri;
                public override System.Uri uri { get { return _uri; } }
                public override object data {
                    get { return _data as Send_Event; }
                    set { _data = value; }
                }
                public override bool IsCritical() { return false; }
            }

            class QueueMsgSendHeartbeat : QueueMsg {
                public static System.Uri _uri;
                public override System.Uri uri { get { return _uri; } }
                public override object data {
                    get { return _data as Game; }
                    set { _data = value; }
                }
                public override bool IsCritical() { return false; }
            }

            class QueueMsgRemoveGame : QueueMsg {
                public static System.Uri _uri;
                public override System.Uri uri { get { return _uri; } }
                public override object data {
                    get { return _data as Game; }
                    set { _data = value; }
                }
                public override bool IsCritical() { return false; }
            }


            class CriticalMessageIllFormedException : System.Exception {
                public CriticalMessageIllFormedException( string message ) : base( message ) { }
            }

            class ServerDownException : System.Exception {
                public ServerDownException( string message ) : base( message ) { }
            }


            // ******************** CONSTANTS ********************

            private const string _SceneObjName = "GameSenseManager_Auto";
            private const string _GameSenseObjName = "GameSenseManager";
            private const uint _MsgQueueSize = 100;
            private const int _ServerProbeInterval = 5000; // 5 seconds
            private const int _MsgCheckInterval = 10;   // 10 ms
            private const long _MaxIdleTimeBeforeHeartbeat = 1000L;   // 1 second

#if UNITY_STANDALONE_WIN
            private const string _ServerPropsPath = "%PROGRAMDATA%/SteelSeries/SteelSeries Engine 3/coreProps.json";
#elif UNITY_STANDALONE_OSX
            private const string _ServerPropsPath = "/Library/Application Support/SteelSeries Engine 3/coreProps.json";
#else
#warning Define server path for your platform
            private const string _ServerPropsPath = "";
#endif



            // ******************** DATA MEMBERS ********************

            private static GSClient _mInstance;
            private ClientState _mClientState;
            private string _mServerPort;

            private System.Threading.Thread _mGameSenseWrk;
            private bool _mGameSenseWrkShouldRun;
            private LocklessQueue< QueueMsg > _mMsgQueue;

            private FullSerializer.fsSerializer _mSerializer;

            private System.Uri _uriBase;




            // ******************** PROPS ********************

            public static GSClient Instance {
                get {
                    if ( _mInstance == null ) {
                        // see if there exists an object with our script attached somewhere
                        // TODO find a better way of doing this
                        _mInstance = FindObjectOfType< GSClient >();

                        if ( _mInstance == null ) {

                            _logWarning( "It appears that the client is not being instantiated as a part of a scene Object. Game registration and handler binding is expected to be done through scripting." );

                            // if no valid instance found, create a dummy GameObject and attach our script to it
                            GameObject obj = new GameObject( _SceneObjName );
                            obj.hideFlags = HideFlags.HideAndDontSave;
                            _mInstance = obj.AddComponent< GSClient >();

                        }
                    }

                    return _mInstance;
                }
            }




            // ******************** METHODS ********************

            private static void _logException( string msg, System.Exception e ) {
#if !SS_NWARN
                Debug.LogWarningFormat( "[GameSense Client] {0} - {1}\n{2}", msg, e.Message, e.StackTrace );
#endif
            }


            private static void _logWarning( string msg ) {
#if !SS_NWARN
                Debug.LogWarningFormat( "[GameSense Client] {0}", msg );
#endif
            }


            private static void _logDbgMsg( string msg ) {
#if SS_DEBUG
                Debug.LogFormat( "[GameSense Client] {0}", msg );
#endif
            }


            private static void _logErrorMsg( string msg ) {
                Debug.LogErrorFormat( "[GameSense Client] {0}", msg );
            }


            private void _setClientState( ClientState state ) {
                _mClientState = state;
            }


            private bool _isClientActive() {
                return _mClientState == ClientState.Active;
            }


            private void _sendServer( System.Uri uri, string data ) {
                byte[] payload = System.Text.Encoding.ASCII.GetBytes( data );

                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create( uri );
                req.ContentType = "application/json";
                req.Method = "POST";

                Stream strm = req.GetRequestStream();
                strm.Write( payload, 0, payload.Length );
                strm.Close();

                System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)req.GetResponse();
#if SS_DEBUG
                strm = resp.GetResponseStream();
                StreamReader sr = new StreamReader( strm, System.Text.Encoding.UTF8 );
                _logDbgMsg( "Server response: " + sr.ReadToEnd() );
                strm.Close();
#endif
                resp.Close();
            }


            private static string _getPropsPath() {
#if UNITY_STANDALONE_WIN
                return System.Environment.ExpandEnvironmentVariables( _ServerPropsPath );
#elif UNITY_STANDALONE_OSX
                return _ServerPropsPath;
#else
                return _ServerPropsPath;
#endif
            }


            private static string _readProps() {
                string propsPath = _getPropsPath();
                string propsJson = null;

                try {
                    propsJson = System.IO.File.ReadAllText( propsPath );
                } catch ( System.Exception e ) {
                    _logException( "Could not read server props file", e );
                }

                return propsJson;
            }


            private static string _getServerPort() {
                string port = null;
                string propsJson = _readProps();

                if ( propsJson != null ) {
                    try {
                        // { address: "127.0.0.1:xxxxx" }
                        coreProps cp = JsonUtility.FromJson< coreProps >( propsJson );
                        string[] addrPort = cp.address.Split( ':' );

                        // this will raise an exception if our input is not what we expect
                        System.Convert.ToUInt16( addrPort[ 1 ] );

                        port = addrPort[ 1 ];
                    } catch ( System.Exception e ) {
                        _logException( "Cannot parse port information", e );
                    }
                }

                return port;
            }


            private void _initializeUris() {
                _uriBase = new System.Uri( "http://127.0.0.1:"+_mServerPort );

                // register uris with corresponding msg types
                QueueMsgRegisterGame._uri = new System.Uri( _uriBase, "game_metadata" );
                QueueMsgBindEvent._uri = new System.Uri( _uriBase, "bind_game_event" );
                QueueMsgRegisterEvent._uri = new System.Uri( _uriBase, "register_game_event" );
                QueueMsgSendEvent._uri = new System.Uri( _uriBase, "game_event" );
                QueueMsgSendHeartbeat._uri = new System.Uri( _uriBase, "game_heartbeat" );
                QueueMsgRemoveGame._uri = new System.Uri( _uriBase, "remove_game" );
            }


            private string _toJSON< T >( T obj ) {
                string serialized = null;
                FullSerializer.fsData fsData;
                FullSerializer.fsResult fsResult;

                fsResult = _mSerializer.TrySerialize< T >( obj, out fsData );

                if ( fsResult.Succeeded ) {
#if SS_DEBUG
                    serialized = FullSerializer.fsJsonPrinter.PrettyJson( fsData );
#else
                    serialized = FullSerializer.fsJsonPrinter.CompressedJson( fsData );
#endif
                } else {
                    throw new System.Exception( "Failed serializing object: " + obj.ToString() );
                }

                return serialized;
            }


            // parse and send msg
            // will raise an exception on any failure
            private void _sendMsg( QueueMsg msg ) {
                string data = _toJSON( msg.data );
                _logDbgMsg( data );

                try {
                    _sendServer( msg.uri, data );
                } catch ( System.Net.WebException e ) {

                    switch ( e.Status ) {

                        // retrieve syntax error message from the server
                        case System.Net.WebExceptionStatus.ProtocolError:
                            if ( msg.IsCritical() ) {
                                System.Net.WebResponse resp = e.Response;
                                Stream strm = resp.GetResponseStream();
                                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);
                                string serverMsg = sr.ReadToEnd();
                                strm.Close();
                                throw new CriticalMessageIllFormedException( serverMsg );
                            }
                            break;

                        case System.Net.WebExceptionStatus.ConnectFailure:
                            throw new ServerDownException( e.Message );

                        // unhandled
                        default:
                            _logException( "Unexpected status", e );
                            throw;

                    }
                }
            }


            private void _addGUIDefinedEvents() {

                if ( GameName == null || GameDisplayName == null || Events == null) {
                    _logWarning( "Incomplete game registration form" );
                    _setClientState( ClientState.Inactive );
                    return;
                }

                RegisterGame( GameName, GameDisplayName, IconColor );

                // TODO need to throttle this so we do not overflow the queue
                //      temporarily mitigated by increasing the queue size
                foreach ( Bind_Event be in Events ) {
                    QueueMsg msg;

                    if ( be.handlers == null || be.handlers.Length == 0 ) {

                        // if no handlers provided, we will at least register these events with SSE
                        msg = new QueueMsgRegisterEvent();
                        msg.data = new Register_Event( GameName, be.eventName, be.minValue, be.maxValue, be.iconId );

                    } else {

                        // bind events with defualt handlers
                        be.game = GameName;
                        msg = new QueueMsgBindEvent();
                        msg.data = be;

                    }

                    _mMsgQueue.PEnqueue( msg );
                }
            }


            private void _gamesenseWrk() {
                QueueMsg pendingMsg = null;
                System.Diagnostics.Stopwatch tLastMsg = new System.Diagnostics.Stopwatch();
                tLastMsg.Start();

                while ( _mGameSenseWrkShouldRun ) {


                    switch ( _mClientState ) {

                        case ClientState.Active:

                            // see if there is any message to process
                            QueueMsg msg;
                            while ( (msg = _mMsgQueue.CDequeue()) == null ) {
                                // no messages in queue, sleep a bit before checking again
                                System.Threading.Thread.Sleep( _MsgCheckInterval );

                                // if a heartbeat event is due, send it now
                                if ( tLastMsg.ElapsedMilliseconds > _MaxIdleTimeBeforeHeartbeat ) {
                                    msg = new QueueMsgSendHeartbeat();
                                    msg.data = new Game( GameName );

                                    break;
                                }
                            }

                            try {

                                // attempt to send a message
                                _sendMsg( msg );

                                // message sent successfully, reset timer
                                tLastMsg.Reset();
                                tLastMsg.Start();

                            } catch ( ServerDownException e ) {

                                // GameSense server down, save the message and go to probing state
                                _logException( "Failed connecting to GameSense server", e );
                                pendingMsg = msg;
                                _setClientState( ClientState.Probing );

                            } catch ( CriticalMessageIllFormedException e ) {

                                // RegisterGame or RegisterEvent or BindEvent messages are not well formed
                                // log and disable
                                _logException( "Message ill-formed", e );
                                _setClientState( ClientState.Inactive );

                            } catch ( System.Exception e ) {
                                _logException( "Failed processing msg", e );
                            }

                            break;

                        case ClientState.Probing:

                            // parse props to get GameSense server port
                            _mServerPort = _getServerPort();
                            if ( _mServerPort == null ) {
                                // SSE3 not installed or coreprops garbled
                                // this failure is beyond anything we can do, GameSense will remain disabled
                                _logWarning( "Failed to obtain GameSense server port. GameSense will not function" );
                                _setClientState( ClientState.Inactive );
                                break;
                            }

                            // port information successfully parsed, init uris
                            _initializeUris();

                            if  ( pendingMsg != null ) {

                                // attempt to send a pending message
                                try {
                                    _sendMsg( pendingMsg );
                                    pendingMsg = null;
                                } catch ( ServerDownException e ) {
                                    // GameSense server down, wait a bit and retry
                                    _logException( "Failed connecting to GameSense server", e );
                                    _logDbgMsg( "Retrying in 5 seconds..." );
                                    System.Threading.Thread.Sleep( _ServerProbeInterval );
                                    break;
                                }

                            }

                            // we can attempt to send data, go to active state
                            _setClientState( ClientState.Active );
                            break;

                        case ClientState.Inactive:
                            // quit
                            _logDbgMsg( "Entering inactive state" );
                            _mGameSenseWrkShouldRun = false;
                            break;

                        default:
                            // unknown state, must abort
                            _logErrorMsg( "Unknown GameSense client state" );
                            _setClientState( ClientState.Inactive );
                            break;

                    }


                }

                _logDbgMsg( "Worker exiting" );

            }


            // Initialization
            void Awake() {

#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED

                if ( _mInstance == null ) {
                    // update the static reference
                    _mInstance = this;
                } else {
                    // no other instances should exist
                    _logWarning( "Another GameSense client script is getting instantiated. Only a single instance should exist at any time" );
                    Destroy( this.gameObject );
                    return;
                }

                // need to survive scene loading
                DontDestroyOnLoad( this.gameObject );

                // initialize
                _mSerializer = new FullSerializer.fsSerializer();
                _mMsgQueue = new LocklessQueue< QueueMsg >( _MsgQueueSize );
                _mGameSenseWrk = new System.Threading.Thread( _gamesenseWrk ); // check for exceptions
                _mGameSenseWrkShouldRun = true;
                _setClientState( ClientState.Probing );

                try {
                    _mGameSenseWrk.Start();

                    // add events if the script is attached to GameSenseManager prefab
                    if ( gameObject.name.Equals( _GameSenseObjName ) ) {
                        _addGUIDefinedEvents();
                    }
                } catch ( System.Exception e ) {
                    _logException( "Could not start the client thread", e );
                    _setClientState( ClientState.Inactive );
                }

#endif  // (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED

            }


            void OnDestroy() {

                _logDbgMsg( "Destroy" );

                if ( _mInstance == this ) {

                    _mGameSenseWrkShouldRun = false;
                    _mInstance = null;

                }
            }


            void OnApplicationQuit() {

                _logDbgMsg( "Quit" );

                // need to destroy here otherwise we may leak resources
                Destroy( this.gameObject );

            }




            // ******************** API ********************

            /// <summary>
            /// Registers a game with GameSense server. Must be called before everything else.
            /// </summary>
            /// <param name="name">Game Identifier. Valid characters are limited to uppercase A-Z, the digits 0-9, hyphen, and underscore</param>
            /// <param name="displayName">The name that will appear in SteelSeries Engine for this game</param>
            /// <param name="iconColor">Color icon identifier that will appear for this game in SteelSeries Engine</param>
            public void RegisterGame( string name, string displayName, IconColor iconColor ) {
#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
                GameName = name.ToUpper();
                GameDisplayName = displayName;
                IconColor = iconColor;

                Register_Game obj = new Register_Game();
                obj.game = GameName;
                obj.game_display_name = GameDisplayName;
                obj.icon_color_id = iconColor;

                QueueMsgRegisterGame msg = new QueueMsgRegisterGame();
                msg.data = obj;

                _mMsgQueue.PEnqueue( msg );
#endif  // (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
            }

            /// <summary>
            /// Defines an event with its handlers and registers them with the GameSense Server.
            /// </summary>
            /// <param name="eventName">Event name. Valid characters are limited to uppercase A-Z, the digits 0-9, hyphen, and underscore</param>
            /// <remarks>
            /// You should not call this function more than once for the same event. Doing so will unbind previously bound handlers.
            /// </remarks>
            /// <param name="minValue">Minimum value</param>
            /// <param name="maxValue">Maximum value</param>
            /// <param name="iconId">Identifies an icon that will be show in SteelSeries Engine for this event</param>
            /// <param name="handlers">An array of handlers for this event</param>
            public void BindEvent( string eventName, System.Int32 minValue, System.Int32 maxValue, EventIconId iconId, AbstractHandler[] handlers ) {
#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
                if ( !_isClientActive() ) return;

                QueueMsgBindEvent msg = new QueueMsgBindEvent();
                msg.data = new Bind_Event( GameName.ToUpper(), eventName.ToUpper(), minValue, maxValue, iconId, handlers );

                _mMsgQueue.PEnqueue( msg );
#endif  // (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
            }

            /// <summary>
            /// Defines an event and registers it with the GameSense Server.
            /// </summary>
            /// <param name="eventName">Event name. Valid characters are limited to uppercase A-Z, the digits 0-9, hyphen, and underscore</param>
            /// <remarks>
            /// You should call this function if you do not want specify default behavior for events but rather want it exposed in SteelSeries Engine for the user to customize.
            /// If you wish to specify default behavior (handlers), use <see cref="GSClient.BindEvent"/> instead
            /// </remarks>
            /// <param name="minValue">Minimum value</param>
            /// <param name="maxValue">Maximum value</param>
            /// <param name="iconId">Identifies an icon that will be show in SteelSeries Engine for this event</param>
            public void RegisterEvent( string eventName, System.Int32 minValue, System.Int32 maxValue, EventIconId iconId ) {
#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
                if ( !_isClientActive() ) return;

                QueueMsgRegisterEvent msg = new QueueMsgRegisterEvent();
                msg.data = new Register_Event( GameName.ToUpper(), eventName.ToUpper(), minValue, maxValue, iconId );

                _mMsgQueue.PEnqueue( msg );
#endif  // (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
            }

            /// <summary>
            /// Sends a new value for the specified event to the GameSense Server
            /// </summary>
            /// <param name="eventName">Previously bound/registered event</param>
            /// <param name="value">New value</param>
            public void SendEvent( string eventName, System.Int32 value ) {
#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
                if ( !_isClientActive() ) return;

                // TODO avoid mem allocations
                Send_Event se = new Send_Event();
                se.game = GameName;
                se.event_name = eventName.ToUpper();
                se.data.value = value;

                QueueMsgSendEvent msg = new QueueMsgSendEvent();
                msg.data = se;

                _mMsgQueue.PEnqueue( msg );
#endif  // (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
            }

            /// <summary>
            /// Notifies the GameSense Server to remove the game and all registered events
            /// </summary>
            public void RemoveGame() {
#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
                if ( !_isClientActive() ) return;

                QueueMsgRemoveGame msg = new QueueMsgRemoveGame();
                msg.data = new Game( GameName );

                _mMsgQueue.PEnqueue( msg );
#endif  // (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !SS_GAMESENSE_DISABLED
            }

        }
    }
}




class LocklessQueue< T > {

    private T[] _bfr;
    private System.Int32 _length;
    private System.Int32 _readIdx;
    private System.Int32 _maxReadIdx;
    private System.Int32 _writeIdx;


    public LocklessQueue( uint size ) {
        _bfr = new T[ size ];
        _length = (System.Int32)size;
        _readIdx = 0;
        _maxReadIdx = 0;
        _writeIdx = 0;
    }

    private System.Int32 index( System.Int32 i ) {
        return i % _length;
    }

    public bool PEnqueue( T obj ) {
        System.Int32 readIdx;
        System.Int32 writeIdx;

        do {
            // TODO beware of overflow
            writeIdx = _writeIdx;
            readIdx = _readIdx;

            if ( index(writeIdx+1) == index(readIdx) ) {
                return false;
            }

            // repeat if _writeIdx has mutated
        } while (System.Threading.Interlocked.CompareExchange(ref _writeIdx, index(writeIdx + 1), writeIdx) != writeIdx);

        _bfr[ index(writeIdx) ] = obj;

        // commit
        while ( System.Threading.Interlocked.CompareExchange(ref _maxReadIdx, index(writeIdx + 1), writeIdx) != writeIdx ) {
           // TODO yield? naaah!
        }

        return true;
    }

    public T CDequeue() {
        T obj = default( T );
        System.Int32 maxReadIdx;
        System.Int32 readIdx;

        do {

            readIdx = _readIdx;
            maxReadIdx = _maxReadIdx;

            if ( index(readIdx) == index(maxReadIdx) ) {
                return default( T );
            }

            obj = _bfr[ index(readIdx) ];

            if ( System.Threading.Interlocked.CompareExchange(ref _readIdx, (readIdx + 1), readIdx) != _readIdx ) {
                break;
            }

        } while ( true );

        return obj;
    }
}
