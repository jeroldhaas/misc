namespace FSharp.Video.Magnification.Eulerian

//open MathNet.Numerics
open System
open System.Collections.Generic
//open Microsoft.FSharp.Collections
//open Microsoft.FSharp.Math
open MathNet.Numerics.LinearAlgebra.Double
open Microsoft.FSharp.Linq.NullableOperators


module MatrixG = Matrix.Generic


type PyrTools =
        // TODO: this is not matrix convolution !!!
        member x.conv(mx:matrix, my:matrix) : matrix =
            mx + my

        member x.binomialFilter size =
            if (size < 2) then
                failwith "size argument must be larger than 1"

            let mutable kernel =
                Matrix.ofList [[0.5; 0.5]]
                |> Matrix.transpose
            let kernelApply =
                Matrix.copy kernel

            for n = 1 to size - 2 do
                kernel <- x.conv(kernelApply, kernel)

        member x.blur(image, ?nlevels, ?filter) =
            let nlevels = defaultArg 1
            let filter = defaultArg "binom5"
            let newFilter = x.namedFilter(filter)
            x.blur(image, nlevels, filter) |> ignore
            
        member x.blur(image, nlevels, filter) =
            filter <- filter / sum(filter((* : *))) // TODO: translate this region
            if (nlevels > 0) then
                if (any(size(image)) = 1) then
                    if (any(size(filter)) = 1) then
                        failwith "Can't apply 2D filter to 1D signal"
                    if (size(image, 2) = 1)
                        failwith "NOT DONE!!!" // TODO: complete transcoding
            1 // TODO: keep the noise down

        member x.mean(m:matrix) =
            let cols = m.NumCols
            let rows = m.NumRows
            Microsoft.FSharp.Math.Matrix.sum(m) / float(cols) * float(rows)
        (*
         * TODO: this is broken, total logic fail
        member x.mean2(mtx:matrix) : matrix =
            x.mean(x.mean mtx)
        *)
            
        member x.namedFilter(name) =
            match name with
            | "binom" -> ignore // TODO: this needs worked out
            | "qmf5"  -> Matrix.ofList [-0.076103; 0.3535534; 0.8593118; 0.3535534; -0.076103]
                            |> Matrix.transpose
            | "qmf9"  -> Matrix.ofList [0.02807382; -0.060944743; -0.073386624; 0.41472545; 0.7973934; 0.41472545 -0.073386624 -0.060944743; 0.02807382]
                            |> Matrix.transpose
            | "qmf13" -> Matrix.ofList [-0.014556438; 0.021651438; 0.039045125; -0.09800052; -0.057827797; 0.42995453; 0.7737113; 0.42995453; -0.057827797; -0.09800052; 0.039045125; 0.021651438; -0.014556438]
                            |> Matrix.transpose
            | "qmf8"  -> sqrt(2) * Matrix.ofList [0.00938715; -0.07065183; 0.06942827; 0.4899808; 0.4899808; 0.06942827; -0.07065183; 0.00938715]
                            |> Matrix.transpose
            | "qmf12" -> sqrt(2) * Matrix.ofList [-0.003809699; 0.01885659; -0.002710326; -0.08469594; 0.08846992; 0.4843894; 0.4843894; 0.08846992; -0.08469594; -0.002710326; 0.01885659; -0.003809699]
                            |> Matrix.transpose
            | "qmf16" -> sqrt(2) * Matrix.ofList [0.001050167; -0.005054526; -0.002589756; 0.0276414; -0.009666376; -0.09039223; 0.09779817; 0.4810284; 0.4810284; 0.09779817; -0.09039223; -0.009666376; 0.0276414; -0.002589756; -0.005054526; 0.001050167]
                            |> Matrix.transpose
            | "haar"  -> Matrix.ofList [1; 1] / sqrt(2)
                            |> Matrix.transpose
            | "daub2" -> Matrix.ofList [0.482962913145; 0.836516303738; 0.224143868042; -0.129409522551]
                            |> Matrix.transpose
            | "daub3" -> Matrix.ofList [0.332670552950; 0.806891509311; 0.459877502118; -0.135011020010; -0.085441273882; 0.035226291882]
                            |> Matrix.transpose
            | "daub4" -> Matrix.ofList [0.230377813309; 0.714846570553; 0.630880767930; -0.027983769417; -0.187034811719; 0.030841381836; 0.032883011667; -0.010597401785]
                            |> Matrix.transpose
            | "gauss5"-> sqrt(2) * Matrix.ofList  [0.0625; 0.25; 0.375; 0.25; 0.0625]
                            |> Matrix.transpose
            | "gauss3"-> sqrt(2) * Matrix.ofList [0.25; 0.5; 0.25]
                            |> Matrix.transpose
            | _       -> failwith "Bad filter name: %A\n", name |> ignore