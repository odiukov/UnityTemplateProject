#!/usr/bin/env bash
echo "${INPUT_CYPHER}" | tr -d '\r' > /Unity_v2019.x.ulf
/opt/Unity/Editor/Unity -manualLicenseFile /Unity_v2019.x.ulf -batchmode -nographics -quit

export BUILD_TARGET=${INPUT_PLATFORM}
export BUILD_NAME=${INPUT_BUILDNAME}
set -e
set -x

mkdir -p $(pwd)/Assets/Editor/
cp /BuildCommand.cs $(pwd)/Assets/Editor/

echo "Building for $BUILD_TARGET"

export BUILD_PATH=./Builds/$BUILD_TARGET/
mkdir -p $BUILD_PATH

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
  -projectPath $(pwd) \
  -quit \
  -batchmode \
  -nographics \
  -buildTarget $BUILD_TARGET \
  -customBuildTarget $BUILD_TARGET \
  -customBuildName $BUILD_NAME \
  -customBuildPath $BUILD_PATH \
  -executeMethod BuildCommand.PerformBuild \
  -logFile /dev/stdout

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $UNITY_EXIT_CODE -eq 3 ]; then
  echo "Run failure (other failure)";
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
fi

echo "::add-path::$BUILD_PATH"

ls -alh $BUILD_PATH
[ -n "$(ls -alh $BUILD_PATH)" ] # fail job if build folder is empty