## About
This plugin is a fork of the plugin [SecondaryAutotype](https://sourceforge.net/projects/keepass-secondaryautotype/). It adds the following custom global hotkeys to KeePass:

- **CTRL+ALT+S:** Triggers the auto-type sequence `{PASSWORD}{ENTER}`
- **CTRL+ALT+Y:** Triggers the auto-type sequence `{TOTP}{ENTER}`
- **CTRL+ALT+P:** Triggers the auto-type sequence `{PICKFIELD}`

## Customizing
The global hotkeys and their corresponding auto-type sequences can be changed in `CustomGlobalHotkeysExt.cs`.

```cs
private void LoadBindings() {
  KeyBindings.Add(new KeyBinding { enabled = true, key = Keys.Control | Keys.Alt | Keys.S, sequence = "{Password}{ENTER}" });
  KeyBindings.Add(new KeyBinding { enabled = true, key = Keys.Control | Keys.Alt | Keys.Y, sequence = "{TOTP}{ENTER}" });
  KeyBindings.Add(new KeyBinding { enabled = true, key = Keys.Control | Keys.Alt | Keys.P, sequence = "{PICKFIELD}" });
}
```

## Building
#### **Requirements:**
- Windows 10
- KeePass v2.46 or later
- git (optional)

#### **Steps:**
1. Run `git clone https://github.com/whalehub/CustomGlobalHotkeys` or download this repository as a .zip file.
2. Run `KeePass.exe --plgx-create "<full path of the cloned/extracted repository>"`.

The compiled .plgx file will be created in the current working directory.
