================================================================================
Actuate Unity Game Engine Asset - v0.1
================================================================================

This package is designed to make it easy to provide Actuate with the telemetry provided from withing the Unity Game Engine.

--------------------------------------------------------------------------------
Actuate
--------------------------------------------------------------------------------
As Actuate is a separate application, the purpose of this asset is to send to data from a Unity application to the Actuate application. Actuate has several input plugins to allow this inter-process communication to take place.

This package provides clients for the standard Actuate input plugins:
Shared Memory
UDP

You must configure Actuate to use one of these input plugins. We recommend the Shared Memory plugin if the Actuate application is running on the same computer as the Unity application.

--------------------------------------------------------------------------------
Unity
--------------------------------------------------------------------------------
Within this package, there is a Prefab provided called ActuateAgent. You should add this prefab to your scene. You should specify the type of client used to send the data setting the ClientType property on your ActuateAgent game object. This setting must match the input plugin that you have selected in Actuate:
"SHARED_MEMORY" = Shared Memory
"UDP = UDP

When you wish to start motion, you should provide the ActuateAgent object with a GameObject to use as a source. You do this by calling the SetMotionSource() method on the ActuateAgent object like so:

actuateAgent.SetMotionSource(player);

if you wish to stop motion, you may use the same method, but provide null as a source:

actuateAgent.SetMotionSource(null);

If you use the UDP ClientType, then you may specify the Host and Port to send the packets to by setting the properties on the UdpClient game object. This is a child of ActuateAgent.

--------------------------------------------------------------------------------
NOTES:
--------------------------------------------------------------------------------
- As part of Actuate, there are some native libraries (libadminclient, libshmemclient) provided that act as clients. These libraries are used via PInvoke, so MUST be available to your Unity game. 
If on Windows, Unity will use the PATH environment variable to locate the libraries. For more information, see http://www.mono-project.com/docs/advanced/pinvoke/

--------------------------------------------------------------------------------
--------------------------------------------------------------------------------
Created by Blueflame Digital Ltd.
http://www.blueflamedigital.co.uk/
--------------------------------------------------------------------------------
--------------------------------------------------------------------------------
