#!/usr/bin/env bash
echo "$UNITY_LICENSE" | tr -d '\r' > /Unity_v2019.x.ulf
/opt/Unity/Editor/Unity -manualLicenseFile /Unity_v2019.x.ulf -batchmode -nographics -quit

set -x

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
  -projectPath $(pwd) \
  -batchmode \
  -enableCodeCoverage \
  -coverageResultsPath $(pwd)/coverage \
  -debugCodeOptimization \
  -coverageOptions generateHtmlReport;generateBadgeReport \
  -quit

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

exit $UNITY_EXIT_CODE