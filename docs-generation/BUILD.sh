echo "Building documentation"
cd ./website
dotnet restore
dotnet build
dotnet run

echo "Copying files into docfx inputs"
cd ../
cp -f ../README.md ./docfx/index.md
cp -f ./website/www/*.md ./docfx/articles/

echo "Building DoxFX. Will Serve"
cd ./docfx
docfx --serve