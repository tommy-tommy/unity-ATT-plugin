#import <Foundation/Foundation.h>
#import <AppTrackingTransparency/ATTrackingManager.h>

#ifdef __cplusplus
extern "C" { 
#endif

int GetTrackingAuthorizationStatus_Dll(){
    if (@available(iOS 14, *)) {
        return (int)ATTrackingManager.trackingAuthorizationStatus;
    } else {
        return -1;
    }
}

typedef void (*Callback)(int status);

void RequestTrackingAuthorization_Dll(Callback callback)
{
    if (@available(iOS 14, *)) {
        [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
            if (callback != nil) {
                callback((int)status);
            }
        }];
    } else {
        callback(-1);
    }
}

#ifdef __cplusplus
}
#endif
