PROJECT=NewsPortal.API
DOCKER_IMAGE=newsportal

build:
	dotnet build $(PROJECT)/$(PROJECT).csproj -c Release

test:
	dotnet test

run:
	dotnet run --project $(PROJECT)

docker-build:
	docker build -t $(DOCKER_IMAGE) .

docker-run:
	docker run --env-file .env -p 8080:8080 $(DOCKER_IMAGE)