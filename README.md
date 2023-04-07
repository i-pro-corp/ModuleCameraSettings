# ModuleCameraSettings

"__Wireless network connection setting file__" creation tool for i-PRO module camera.

---

## Summary

- This is a tool to create a "__wireless network connection setting file__" for i-PRO module cameras.
- "__Wireless network connection setting file__" is a file used for wireless connection settings of i-PRO module cameras.

<img alt="tool_screen_image.png" src="image/tool_screen_image.png" width="500px">

---

## How to use ModuleCameraSettings

1. Launch the "ModuleCameraSettings" tool.
1. Enter the information required for wireless connection on the tool screen.
1. Click the [Save file] button to save the "wireless network connection setting file".
     - __IMPORTANT__: You MUST save the filename as "MCS-INIT".
1. Save the "wireless network connection setting file" to the microSD.
     - __IMPORTANT__: You MUST save to the root of the microSD.
1. Insert the microSD into the i-PRO camera body.
1. Turn on the power of the i-PRO camera. Automatically configure wireless settings for i-PRO cameras.
1. Setting completed

---

## The way to build ModuleCameraSettings

You can build the source code using Visual Studio 2019/2022 or by following the steps below.

1. Download and install the latest version of .NET SDK corresponding to architecture.  
[Download .NET 7.0 (Linux, macOS, and Windows) (microsoft.com)
](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
<img alt="how_to_download_sdk.jpg" src="image/how_to_download_sdk.jpg" width="500px">

2. Launch PowerShell, and execute the command "dotnet build -c Release" in the folder for saved "ModuleCameraSettings.sln".
<img alt="PowerShell_outout_sample.jpg" src="image/PowerShell_output_sample.jpg" width="500px">
