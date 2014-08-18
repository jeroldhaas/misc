namespace UnityEngine.FSharp

open UnityEngine
open System.Collections

type public RotationAxes =
    | MouseXandY    = 0
    | MouseX        = 1
    | MouseY        = 2

[<AddComponentMenu("Camera-Control/Mouse Look")>]
type public MouseLook() =
    inherit UnityEngine.MonoBehaviour()

    member val Axes = RotationAxes.MouseXandY with get, set
    member val SensitivityX = 15.0f with get, set
    member val SensitivityY = 15.0f with get, set
    member val MinimumX = -360.0f with get, set
    member val MaximumX = 360.0f with get, set
    member val MinimumY = 60.0f with get, set
    member val MaximumY = 0.0f with get, set
    member val RotationY = 0.0f with get, set
    member val RotationX = 0.0f with get, set
        
    member x.Update() =
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
                
   (*
                
    member x.Start() : void =
        if (x.rigidbody) then
            rigidbody.freeze
    *)