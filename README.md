# Simple Architechture System
This is a simple architechture using Service locator pattern to locate all services on scene. By using Tag and Scriptable object as enum, you can get reference to a service.

# Scene Core
- Service container on the scene.
- Only one per scene
- Default execution -2 so it will called first before any monobehaviour class on the scene, by doing that SceneCore will collect and Initialize every service on the scene before used by any class.
- Using tag 'Scene Core'

# Scene Service
- Replacement of singleton
- Every manager/service class will inherit this SceneService class
- Hold Service ID (scriptable object) as ID to be referenced by other class
- Using tag 'SceneService'
