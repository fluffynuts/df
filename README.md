df
---

A very simple `df`-like clone for windows. No options, no frills, just see drive usage.

building
---
You'll need at least dotnet 6.0. If you like an easy life, install node:
```
npm ci
npm run publish
```

(artifact will appear in the `Publish` folder)

if you don't have (and really don't want to install) node, check out the commandline
for the "publish" script in package.json, or roll your own.

usage
---

Try `--help`.

Run `df` to get a human-readable output. You can get byte sizes with `--no-human-readable`
or use a 1000 divisor instead of the generally-accepted 1024 with `--si`.

it's fat
---

yes, this is a chonky boi when published (11mb), but it was quick to bang out in .net - if you
want to keep it smaller, you probably only need:
- df.exe
- df.dll
- PeanutButter.EasyArgs.dll
- PeanutButter.DuckTyping.dll

for a total of about 675k, which is still chonky compared with the native `df` coming
in at about 2k. You don't _have_ to use it (:
