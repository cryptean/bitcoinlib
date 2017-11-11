#!/bin/sh -eu

if [ -z "${COMSPEC-}" ]; then
  run="mono"
else
  run=""
fi

# Only run bootstrapper if we haven't done so in the last 24 hours, this keeps builds fast.
if ! test -f .paket/paket.bootstrapper.run || find .paket/paket.bootstrapper.run -type f -mtime +1 | grep -q paket.bootstrapper.run; then
    $run .paket/paket.bootstrapper.exe
    touch .paket/paket.bootstrapper.run
fi

$run .paket/paket.exe restore
$run packages/FAKE/tools/FAKE.exe $@
