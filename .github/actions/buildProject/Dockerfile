# Container image that runs your code
FROM gableroux/unity3d:2019.4.10f1-android

RUN apt-get -y update && apt-get -y install git-core

# Copies your code file from your action repository to the filesystem path `/` of the container
COPY build.sh /build.sh
COPY BuildCommand.cs /BuildCommand.cs

# Code file to execute when the docker container starts up (`build.sh`)
ENTRYPOINT ["/build.sh"]