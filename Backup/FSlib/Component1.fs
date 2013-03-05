namespace FSlib

open UnityEngine
open System.Collections

[<AddComponentMenu("Camera-Control/Mouse Look")>]
type MouseLook() = inherit MonoBehaviour()

    type RotationAxes =
    {
            MouseXandY = 0,
            MouseX = 1,
            MouseY = 2
        }
    member x.Axes = Rotation
    member x.Update() : void =
        if (axes
        
    