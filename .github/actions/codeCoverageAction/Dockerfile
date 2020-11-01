# Container image that runs your code
FROM gableroux/unity3d:2019.4.13f1

RUN apt-get -y update && apt-get -y install git-core

# Copies your code file from your action repository to the filesystem path `/` of the container
COPY test.sh /test.sh

# Code file to execute when the docker container starts up (`build.sh`)
ENTRYPOINT ["/test.sh"]