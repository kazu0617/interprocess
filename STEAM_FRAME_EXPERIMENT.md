# Archived Steam Frame semaphore experiment

This branch preserves the uncommitted diagnostic change from the 2026-09-26
Resonite / Steam Frame investigation. Base: `61858b302522ca2c6f092e6a81afc51ac9a45076`.

The change selects Windows named semaphores whenever Wine is detected, avoiding
the x86-64 ELF helper that ARM64 Wine cannot load. This is an archived experiment,
not a proposed general fix: a native Linux host and a Wine renderer can end up
using different semaphore namespaces. The investigation report explicitly
rejected unconditional Wine-to-Windows semaphore switching for normal use.

The later PE frontend + native Unixlib bridge experiment preserves the POSIX
semaphore interface. Its source and the auxiliary .NET patch tools are archived
in `kazu0617/Wine-ShmBridge`, branch `experiment/steam-frame-unixlib`.

Validation during archival: `git diff --check`. No .NET build or device runtime
test was rerun. The historical investigation reached stable process / IPC startup,
but the renderer remained black; successful UI or world rendering was not shown.
