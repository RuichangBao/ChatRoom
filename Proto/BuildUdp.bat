@echo off
set outPut=csharpOutPut
set clientOutPut=..\UdpClient\Assets\Scripts\Proto
set serverOutPut=..\UdpServer\Server\Proto

echo remove file in csharpOutPut
if exist %outPut% (
    del /f /s /q "%outPut%\*"
) else (
    mkdir %outPut%
)

echo compile propt Start
for %%i in (*.proto) do (
    echo %%i
    protoc.exe --csharp_out=%outPut% %%i
)
echo compile propt End

echo remove file in Client
if exist %clientOutPut% (
    del /f /s /q "%clientOutPut%\*"
) else (
    mkdir %clientOutPut%
)

echo remove file in Server
if exist %serverOutPut% (
    del /f /s /q "%serverOutPut%\*"
) else (
    mkdir %serverOutPut%
)

echo copy proto
for %%i in ("%outPut%\*") do (
    copy %%i %serverOutPut%\
    copy %%i %clientOutPut%\
)

echo "press any key to exit"
pause