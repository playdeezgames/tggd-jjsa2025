rm -rf ./pub-html
dotnet publish ./src/Contemn/Contemn.csproj -o ./pub-html -c Release 
dotnet publish ./src/Grubstaker/Grubstaker.vbproj -o ./pub-linux -c Release --sc -p:PublishSingleFile=true -r linux-x64
dotnet publish ./src/Grubstaker/Grubstaker.vbproj -o ./pub-windows -c Release --sc -p:PublishSingleFile=true -r win-x64
dotnet publish ./src/Grubstaker/Grubstaker.vbproj -o ./pub-mac -c Release --sc -p:PublishSingleFile=true -r osx-x64
dotnet publish ./src/GrubstakerDemo/GrubstakerDemo.vbproj -o ./pubdemo-linux -c Release --sc -p:PublishSingleFile=true -r linux-x64
dotnet publish ./src/GrubstakerDemo/GrubstakerDemo.vbproj -o ./pubdemo-windows -c Release --sc -p:PublishSingleFile=true -r win-x64
dotnet publish ./src/GrubstakerDemo/GrubstakerDemo.vbproj -o ./pubdemo-mac -c Release --sc -p:PublishSingleFile=true -r osx-x64
rm -f ./pub-html/*.pdb
butler push pub-html/wwwroot thegrumpygamedev/tggd-jjsa2025:html
butler push pub-windows thegrumpygamedev/tggd-jjsa2025:windows
butler push pub-mac thegrumpygamedev/tggd-jjsa2025:mac
butler push pub-linux thegrumpygamedev/tggd-jjsa2025:linux
butler push pubdemo-windows thegrumpygamedev/tggd-jjsa2025:demo-windows
butler push pubdemo-mac thegrumpygamedev/tggd-jjsa2025:demo-mac
butler push pubdemo-linux thegrumpygamedev/tggd-jjsa2025:demo-linux
