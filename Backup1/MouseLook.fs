//namespace FSlib


open UnityEngine
open System.Collections

type public RotationAxes =
    | MouseXandY    = 0
    | MouseX        = 1
    | MouseY        = 2


[<AddComponentMenu("Camera-Control/Mouse Look")>]
type public MouseLook() =
    inherit UnityEngine.MonoBehaviour()

    let axes = RotationAxes.MouseXandY
    let sensitivityX = 15.0f
    let sensitivityY = 15.0f
    let minimumX = -360.0f
    let maximumX = 360.0f
    let minimumY = -60.0f
    let maximumY = 60.0f
    let rotationY = 0.0f
    let rotationX = 0.0f
    

    member x.Axes 
        with get() = axes and set(s) = ()
    member x.SensitivityX 
        with get() = sensitivityX and set(s) = ()
    member x.SensitivityY 
        with get() = sensitivityY and set(s) = ()
    member x.MinimumX
        with get() = minimumX and set(s) = ()
    member x.MaximumX 
        with get() = maximumX and set(s) = ()
    member x.MinimumY 
        with get() = minimumY and set(s) = ()
    member x.MaximumY
        with get() = maximumY and set(s) = ()
    member x.RotationY 
        with get() = rotationY and set(s) = ()
    member x.RotationX
        with get() = rotationX and set(s) = ()
        
        
    member x.Update() : void =
        if (x.Axes = RotationAxes.MouseXandY) then
            x.RotationX <- x.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * x.SensitivityX

            x.RotationY <- x.RotationY + Input.GetAxis("Mouse Y") * x.SensitivityY
            x.RotationY <- Mathf.Clamp(x.RotationY, x.MinimumY, x.MaximumY)

            x.transform.localEulerAngles <- new Vector3(-x.RotationY, x.RotationX, 0.0f)
        else if (x.Axes = RotationAxes.MouseX) then
            x.transform.Rotate(0.0f, Input.GetAxis("Mouse X") * x.SensitivityX, 0.0f)
        else
            x.RotationY <- x.RotationY + Input.GetAxis("Mouse Y") * x.SensitivityY
            x.RotationY <- Mathf.Clamp(x.RotationY, x.MinimumY, x.MaximumY)

            x.transform.localEulerAngles <-
                new Vector3(-x.RotationY, x.transform.localEulerAngles.y, 0.0f)
                
                
    member x.Start() : void =
        if (UnityEngine.) then
            rigidbody.