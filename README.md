# Unity ATT Plugin

This plugin implements the display of the tracking request modal in iOS.

![screen_shot](https://user-images.githubusercontent.com/49640196/151963131-82554eab-f419-4eb5-a417-543cd8368a5f.png)


# Installation

Import the [unitypackage](https://github.com/tommy-tommy/unity-ATT-plugin/releases).

# Usage

1. "GetTrackingAuthorizationStatus" to get the current authorization status.

2. If it is null, it is still in an unrequested state. Use "ATTService.RequestTrackingAuthorization" to display the request modal.

<strong>No special processing is required after the build.</strong>

## Example

```bash
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

Confirmed to work with Unity 2018.4.36

# License

This is under [MIT license](https://en.wikipedia.org/wiki/MIT_License).
