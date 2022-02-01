# Unity ATT Plugin

### This plugin implements the display of the tracking request modal in iOS.  
<br>

![screen_shot](https://user-images.githubusercontent.com/49640196/151963131-82554eab-f419-4eb5-a417-543cd8368a5f.png)


# Installation

Import the [unitypackage](https://github.com/tommy-tommy/unity-ATT-plugin/releases).

# Usage

Use the `ATTService` class.

1. `ATTService.GetTrackingAuthorizationStatus()` to get the current authorization status.

2. If it is null, it is still in an unrequested state. Use `ATTService.RequestTrackingAuthorization()` to display the request modal.

---

A description of the usage is provided in the `ATTSettings.asset` in the `Assets/Plugins/ATT` folder.

![image](https://user-images.githubusercontent.com/49640196/151977112-9c41183e-da74-40d2-9192-33c86fad5225.png)

***
**There is nothing to do after the build.**  
***
<br>


## Example

```C#
    // using ATT;
    var ret = ATTService.GetTrackingAuthorizationStatus();
    if (!ret.HasValue)
    {
        Debug.Log("ATT unrequested");
        ATTService.RequestTrackingAuthorization(OnComplete);
    }
    else
    {
        Debug.Log("ATT requested");
        OnComplete((bool)ret);
    }

    void OnComplete(bool result)
    {
        if (result)
        {
            Debug.Log("ATT permitted");
        }
        else
        {
            Debug.Log("ATT not permitted");
        }
    }
```

# Requirement

Confirmed to work with Unity 2019.3.9

# License

This is under [MIT license](https://en.wikipedia.org/wiki/MIT_License).
