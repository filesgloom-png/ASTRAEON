# ASTRAEON

Mobile dark-fantasy MMORPG client prototype. Unity 6 + URP, Android ARM64, landscape.

## Stage 1
- Establish a clean Unity project structure.
- Runtime bootstrap creates a polished first-launch title screen without requiring a binary scene asset.
- Android-oriented settings and CI validation.
- APK export is conditional on a valid Unity license being configured in GitHub Actions.

## Android build prerequisite
Unity's editor/build license is required for a real APK build. Configure a repository Actions secret named `UNITY_LICENSE` with a valid Unity license file. Without it, CI performs source/project validation and explicitly reports that APK export was skipped.

## Direction
The first milestone is the client foundation and title screen. Character creation, six-class selection, nickname flow, and the 3D world will be introduced in later verified milestones.
