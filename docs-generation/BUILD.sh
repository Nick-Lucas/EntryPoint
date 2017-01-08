echo "Building documentation"
cd ./Website
dotnet restore
dotnet build
dotnet run

cp ./www/body.md ../docfx/articles/body.md

echo "Building DoxFX and passing on all arguments"
cd ../docfx
docfx "$@"