# Introduction
This project was created using the Microservice Template Application (`avalonmicroservice`).

# Getting Started
1.	Implement logic
2.	Use \*.Tests project to create there unit and narrow integration tests
3.  Don't forget to update `appsettings.json`
4.  Don't put secrets in `appsettings.json`
5.  Describe you microservice in README

# Project structure:
MicroserviceTemplate/
├── Dockerfile
├── src/
│   └── MicroserviceTemplate/
│       ├── MicroserviceTemplate.csproj
│       └── (other project files)
└── test/
    └── MicroserviceTemplate.Tests/
        ├── MicroserviceTemplate.Tests.csproj
        └── (other test project files)


# Docker
## Build
-t accounts-service:latest — tag (name) for Docker image.
. — Docker specifies that the context build is in the current directory (where the Dockerfile is located).

docker build -t accounts-service:latest .


## Run
-d — runs a container in the background.
-p 5001:80 — forwards port 80 inside the container to port 5000 on your machine. You can configure the ports as you wish.
--name accounts-service — specifies a name for the container.
  accounts-service:latest — the name and tag of the Docker image you created in the previous step.

docker run -d -p 5001:8080 --name accounts-container accounts-service:latest


## Check 
docker ps
curl http://localhost:5001/api/ping

## Logs
docker logs accounts-container


## Stop and Delete
docker stop accounts-container
docker rm accounts-container

## Remove Image
docker rmi accounts-service:latest