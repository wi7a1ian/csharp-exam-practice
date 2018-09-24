call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\Tools\VsDevCmd.bat"
sn -v bin/Release/StrongNamedAssembly.dll
sn -q -T bin/Release/StrongNamedAssembly.dll
sn -q -t SignKeyTest.pfx
rem sn -e bin/Release/StrongNamedAssembly.dll bin/Release/PublicKey.key
rem sn -p SignKeyTest.pfx bin/Release/PublicKey2.key
pause