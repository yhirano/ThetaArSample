#import <Foundation/Foundation.h>
#import <CoreMotion/CoreMotion.h>

BOOL IsSupportsGyro(void);

void StartGyro(void);

void StopGyro(void);

void GetGyroQuaternion(float* result);

@interface GyroCameraController : NSObject

@property (readonly, nonatomic) CMQuaternion quaternion;

- (BOOL)deviceMotionAvailable;

- (void)start;

- (void)stop;

@end

