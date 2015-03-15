#import "GyroCameraController.h"

@implementation GyroCameraController
{
    CMMotionManager *motionManager_;
}

- (id)init {
    if (self = [super init]) {
        motionManager_ = [[CMMotionManager alloc] init];
    }
    return self;
}

- (BOOL)deviceMotionAvailable {
    return motionManager_.deviceMotionAvailable;
}

- (CMQuaternion)quaternion {
    CMAttitude* attitude = motionManager_.deviceMotion.attitude;
    if (!attitude) {
        CMQuaternion result;
        result.x = result.y = result.z = result.w = 0;
        return result;
    }
    
    return attitude.quaternion;
}

- (void)start {
    if (![self deviceMotionAvailable]) {
        return;
    }

    // accelerometer + gyro
    [motionManager_ startDeviceMotionUpdatesUsingReferenceFrame: CMAttitudeReferenceFrameXArbitraryCorrectedZVertical];

    // accelerometer + gyro + compass
    //[motionManager_ startDeviceMotionUpdatesUsingReferenceFrame:CMAttitudeReferenceFrameXMagneticNorthZVertical];
    //[motionManager_ startDeviceMotionUpdatesUsingReferenceFrame:CMAttitudeReferenceFrameXTrueNorthZVertical];
}

- (void)stop {
    [motionManager_ stopDeviceMotionUpdates];
}

@end

static GyroCameraController* instance = nil;

static GyroCameraController* SharedInstance(void) {
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[GyroCameraController alloc] init];
    });
    return instance;
}

BOOL IsSupportsGyro(void) {
    return [SharedInstance() deviceMotionAvailable];
}

void StartGyro(void) {
    [SharedInstance() start];
}

void StopGyro(void) {
    [SharedInstance() stop];
}

void GetGyroQuaternion(float* result) {
    if (result == nil || result == NULL) {
        return;
    }
    
    CMQuaternion quaternion = SharedInstance().quaternion;
    result[0] = quaternion.x;
    result[1] = quaternion.y;
    result[2] = quaternion.z;
    result[3] = quaternion.w;
}
