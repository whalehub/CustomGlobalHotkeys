using System.Windows.Forms;
using System.Collections.Generic;
using KeePass.Plugins;
using KeePass.Util;

namespace CustomGlobalHotkeys {
  public class KeyBinding {
    public bool enabled;
    public Keys key;
    public string sequence;
    public int id = -1000;
  }

  public sealed class CustomGlobalHotkeysExt: Plugin {
    private IPluginHost m_host = null;
    private string MySequence;
    private List < KeyBinding > KeyBindings = new List < KeyBinding > ();

    public override string UpdateUrl {
      get { return "https://raw.githubusercontent.com/whalehub/CustomGlobalHotkeys/master/version_manifest.txt"; }
    }

    public override bool Initialize(IPluginHost host) {
      m_host = host;
      MySequence = "";
      LoadBindings();
      HotKeyInternals.HotKeyPressed += HotKeyPressed;
      AutoType.FilterCompilePre += FilterCompilePre;

      foreach(KeyBinding binding in KeyBindings) binding.id = HotKeyInternals.InternalRegisterHotKey(binding.key);
      return true;
    }

    public override void Terminate() {
      foreach(KeyBinding binding in KeyBindings) HotKeyInternals.InternalUnregisterHotKey(binding.id);
      HotKeyInternals.HotKeyPressed -= HotKeyPressed;
      AutoType.FilterCompilePre -= FilterCompilePre;
    }

    private void FilterCompilePre(object sender, AutoTypeEventArgs e) {
      if (MySequence != "") e.Sequence = MySequence;
      MySequence = "";
    }

    private void HotKeyPressed(object sender, HotKeyEventArgs e) {
      Keys key = e.Key;

      if (0 != (e.Modifiers & KeyModifiers.Alt)) key |= Keys.Alt;
      if (0 != (e.Modifiers & KeyModifiers.Control)) key |= Keys.Control;
      if (0 != (e.Modifiers & KeyModifiers.Shift)) key |= Keys.Shift;

      foreach(KeyBinding binding in KeyBindings) {
        if (binding.enabled && (binding.key == key)) {
          MySequence = binding.sequence;
          break;
        }
      }

      m_host.MainWindow.ExecuteGlobalAutoType();
      MySequence = "";
    }

    private void LoadBindings() {
      KeyBindings.Add(new KeyBinding { enabled = true, key = Keys.Control | Keys.Alt | Keys.S, sequence = "{Password}{ENTER}" });
      KeyBindings.Add(new KeyBinding { enabled = true, key = Keys.Control | Keys.Alt | Keys.Y, sequence = "{TOTP}{ENTER}" });
      KeyBindings.Add(new KeyBinding { enabled = true, key = Keys.Control | Keys.Alt | Keys.P, sequence = "{PICKFIELD}" });
    }
  }
}
