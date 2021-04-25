using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WindowsInput;
using WpfKb.LogicalKeys;

namespace WpfKb.Controls
{
    public class OnScreenWebKeyboard : Grid
    {
        public static readonly DependencyProperty AreAnimationsEnabledProperty = DependencyProperty.Register("AreAnimationsEnabled", typeof(bool), typeof(OnScreenWebKeyboard), new UIPropertyMetadata(true, OnAreAnimationsEnabledPropertyChanged));

        private ObservableCollection<OnScreenKeyboardSection> _sections;
        private List<ModifierKeyBase> _modifierKeys;
        private List<ILogicalKey> _allLogicalKeys;
        private List<OnScreenKey> _allOnScreenKeys;

        public bool AreAnimationsEnabled
        {
            get { return (bool)GetValue(AreAnimationsEnabledProperty); }
            set { SetValue(AreAnimationsEnabledProperty, value); }
        }

        private static void OnAreAnimationsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var keyboard = (OnScreenWebKeyboard)d;
            keyboard._allOnScreenKeys.ToList().ForEach(x => x.AreAnimationsEnabled = (bool)e.NewValue);
        }

        public override void BeginInit()
        {
            SetValue(FocusManager.IsFocusScopeProperty, true);
            _modifierKeys = new List<ModifierKeyBase>();
            _allLogicalKeys = new List<ILogicalKey>();
            _allOnScreenKeys = new List<OnScreenKey>();

            _sections = new ObservableCollection<OnScreenKeyboardSection>();

            var mainSection = new OnScreenKeyboardSection();
            var mainKeys = new ObservableCollection<OnScreenKey>
                               {
                                   new OnScreenKey { GridRow = 0, GridColumn = 0, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_3, new List<string> { "` ذ", "~" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 1, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_1, new List<string> { "1", "!" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 2, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_2, new List<string> { "2", "@" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 3, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_3, new List<string> { "3", "#" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 4, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_4, new List<string> { "4", "$" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 5, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_5, new List<string> { "5", "%" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 6, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_6, new List<string> { "6", "^" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 7, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_7, new List<string> { "7", "&" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 8, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_8, new List<string> { "8", "*" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 9, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_9, new List<string> { "9", "(" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 10, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_0, new List<string> { "0", ")" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 11, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_MINUS, new List<string> { "-", "_" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 12, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_PLUS, new List<string> { "=", "+" })},
                                   new OnScreenKey { GridRow = 0, GridColumn = 13, Key =  new VirtualKey(VirtualKeyCode.BACK, "Bksp"), GridWidth = new GridLength(2, GridUnitType.Star)},

                                   new OnScreenKey { GridRow = 1, GridColumn = 0, Key =  new VirtualKey(VirtualKeyCode.TAB, "Tab"), GridWidth = new GridLength(1.5, GridUnitType.Star)},
                                   new OnScreenKey { GridRow = 1, GridColumn = 1, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_Q, new List<string> { "q ض", "Q َ" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 2, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_W, new List<string> { "w ص", "W ً" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 3, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_E, new List<string> { "e ث", "E ُ" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 4, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_R, new List<string> { "r ق", "R ٌ" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 5, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_T, new List<string> { "t ف", "T لإ" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 6, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_Y, new List<string> { "y غ", "Y إ" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 7, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_U, new List<string> { "u ع", "U ‘" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 8, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_I, new List<string> { "i ه", "I ÷" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 9, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_O, new List<string> { "o خ", "O ×" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 10, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_P, new List<string> { "p ح", "P ؛" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 11, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_4, new List<string> { "[ ج", "{ }" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 12, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_6, new List<string> { "] د", "} {" })},
                                   new OnScreenKey { GridRow = 1, GridColumn = 13, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_5, new List<string> { "\\", "| ّ" }), GridWidth = new GridLength(1.3, GridUnitType.Star)},

                                   new OnScreenKey { GridRow = 2, GridColumn = 0, Key =  new TogglingModifierKey("Caps", VirtualKeyCode.CAPITAL), GridWidth = new GridLength(1.7, GridUnitType.Star)},
                                   new OnScreenKey { GridRow = 2, GridColumn = 1, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_A, new List<string> { "a ش", "A \\" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 2, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_S, new List<string> { "s س", "S " })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 3, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_D, new List<string> { "d ي", "D ]" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 4, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_F, new List<string> { "f ب", "F [" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 5, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_G, new List<string> { "g ل", "G لأ" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 6, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_H, new List<string> { "h ا", "H أ" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 7, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_J, new List<string> { "j ت", "J ـ" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 8, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_K, new List<string> { "k ن", "K ،" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 9, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_L, new List<string> { "l م", "L /" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 10, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_1, new List<string> { "; ك", ":" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 11, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_7, new List<string> { "\" ط", "\"" })},
                                   new OnScreenKey { GridRow = 2, GridColumn = 12, Key =  new VirtualKey(VirtualKeyCode.RETURN, "Enter"), GridWidth = new GridLength(1.8, GridUnitType.Star)},

                                   new OnScreenKey { GridRow = 3, GridColumn = 0, Key =  new InstantaneousModifierKey("Shift", VirtualKeyCode.SHIFT), GridWidth = new GridLength(2.4, GridUnitType.Star)},
                                   new OnScreenKey { GridRow = 3, GridColumn = 1, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_Z, new List<string> { "z ئ", "Z ~" })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 2, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_X, new List<string> { "x ء", "X ْ" })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 3, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_C, new List<string> { "c ؤ", "C ِ" })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 4, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_V, new List<string> { "v ر", "V ٍ" })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 5, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_B, new List<string> { "b لا", "B لآ" })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 6, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_N, new List<string> { "n ى", "N آ" })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 7, Key =  new ShiftSensitiveKey(VirtualKeyCode.VK_M, new List<string> { "m ة", "M ’" })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 8, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_COMMA, new List<string> { ", و", "< ," })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 9, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_PERIOD, new List<string> { ". ز", "> ." })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 10, Key =  new ShiftSensitiveKey(VirtualKeyCode.OEM_2, new List<string> { "/ ظ", "?" })},
                                   new OnScreenKey { GridRow = 3, GridColumn = 11, Key =  new InstantaneousModifierKey("Shift", VirtualKeyCode.SHIFT), GridWidth = new GridLength(2.4, GridUnitType.Star)},

                                   new OnScreenKey { GridRow = 4, GridColumn = 0, Key =  new VirtualKey(VirtualKeyCode.SPACE, " "), GridWidth = new GridLength(5, GridUnitType.Star)},
                                   new OnScreenKey { GridRow = 4, GridColumn = 1, Key =  new TogglingModifierKey("lang", VirtualKeyCode.MENU)},
                                  // new OnScreenKey { GridRow = 4, GridColumn = 2, Key =  new StringKey("https://", "https://")},
                               };

            mainSection.Keys = mainKeys;
            mainSection.SetValue(ColumnProperty, 0);
            _sections.Add(mainSection);
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            Children.Add(mainSection);

            _allLogicalKeys.AddRange(mainKeys.Select(x => x.Key));
            _allOnScreenKeys.AddRange(mainSection.Keys);


            var specialSection = new OnScreenKeyboardSection();
            var specialKeys = new ObservableCollection<OnScreenKey>
                                  {
                                      new OnScreenKey { GridRow = 0, GridColumn = 0, Key = new ChordKey("Select All", VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A), GridWidth = new GridLength(2, GridUnitType.Star)},
                                      new OnScreenKey { GridRow = 0, GridColumn = 1, Key = new ChordKey("Undo", VirtualKeyCode.CONTROL, VirtualKeyCode.VK_Z) },
                                      new OnScreenKey { GridRow = 1, GridColumn = 0, Key = new ChordKey("Copy", VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C) },
                                      new OnScreenKey { GridRow = 1, GridColumn = 1, Key = new ChordKey("Cut", VirtualKeyCode.CONTROL, VirtualKeyCode.VK_X) },
                                      new OnScreenKey { GridRow = 1, GridColumn = 2, Key = new ChordKey("Paste", VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V) },
                                      new OnScreenKey { GridRow = 2, GridColumn = 0, Key = new VirtualKey(VirtualKeyCode.DELETE, "Del") },
                                      new OnScreenKey { GridRow = 2, GridColumn = 1, Key = new VirtualKey(VirtualKeyCode.HOME, "Home") },
                                      new OnScreenKey { GridRow = 2, GridColumn = 2, Key = new VirtualKey(VirtualKeyCode.END, "End") },
                                      new OnScreenKey { GridRow = 3, GridColumn = 0, Key = new VirtualKey(VirtualKeyCode.PRIOR, "PgUp") },
                                      new OnScreenKey { GridRow = 3, GridColumn = 1, Key = new VirtualKey(VirtualKeyCode.UP, "Up") },
                                      new OnScreenKey { GridRow = 3, GridColumn = 2, Key = new VirtualKey(VirtualKeyCode.NEXT, "PgDn") },
                                      new OnScreenKey { GridRow = 4, GridColumn = 0, Key = new VirtualKey(VirtualKeyCode.LEFT, "Left") },
                                      new OnScreenKey { GridRow = 4, GridColumn = 1, Key = new VirtualKey(VirtualKeyCode.DOWN, "Down") },
                                      new OnScreenKey { GridRow = 4, GridColumn = 2, Key = new VirtualKey(VirtualKeyCode.RIGHT, "Right") },
                                  };

            specialSection.Keys = specialKeys;
            specialSection.SetValue(ColumnProperty, 1);
            _sections.Add(specialSection);
            ColumnDefinitions.Add(new ColumnDefinition());
            Children.Add(specialSection);

            _allLogicalKeys.AddRange(specialKeys.Select(x => x.Key));
            _allOnScreenKeys.AddRange(specialSection.Keys);



            _modifierKeys.AddRange(_allLogicalKeys.OfType<ModifierKeyBase>());
            _allOnScreenKeys.ForEach(x => x.OnScreenKeyPress += OnScreenKeyPress);

            SynchroniseModifierKeyState();

            base.BeginInit();
        }

        void OnScreenKeyPress(DependencyObject sender, OnScreenKeyEventArgs e)
        {
            if (e.OnScreenKey.Key is ModifierKeyBase)
            {
                var modifierKey = (ModifierKeyBase)e.OnScreenKey.Key;
                if (modifierKey.KeyCode == VirtualKeyCode.SHIFT)
                {
                    HandleShiftKeyPressed(modifierKey);
                }
                else if (modifierKey.KeyCode == VirtualKeyCode.CAPITAL)
                {
                    HandleCapsLockKeyPressed(modifierKey);
                }
                else if (modifierKey.KeyCode == VirtualKeyCode.NUMLOCK)
                {
                    HandleNumLockKeyPressed(modifierKey);
                }
            }
            else
            {
                ResetInstantaneousModifierKeys();
            }
            _modifierKeys.OfType<InstantaneousModifierKey>().ToList().ForEach(x => x.SynchroniseKeyState());
        }

        private void SynchroniseModifierKeyState()
        {
            _modifierKeys.ToList().ForEach(x => x.SynchroniseKeyState());
        }

        private void ResetInstantaneousModifierKeys()
        {
            _modifierKeys.OfType<InstantaneousModifierKey>().ToList().ForEach(x => { if (x.IsInEffect) x.Press(); });
        }

        void HandleShiftKeyPressed(ModifierKeyBase shiftKey)
        {
            _allLogicalKeys.OfType<CaseSensitiveKey>().ToList().ForEach(x => x.SelectedIndex =
                                                                             InputSimulator.IsTogglingKeyInEffect(VirtualKeyCode.CAPITAL) ^ shiftKey.IsInEffect ? 1 : 0);
            _allLogicalKeys.OfType<ShiftSensitiveKey>().ToList().ForEach(x => x.SelectedIndex = shiftKey.IsInEffect ? 1 : 0);
        }

        void HandleCapsLockKeyPressed(ModifierKeyBase capsLockKey)
        {
            _allLogicalKeys.OfType<CaseSensitiveKey>().ToList().ForEach(x => x.SelectedIndex =
                                                                             capsLockKey.IsInEffect ^ InputSimulator.IsKeyDownAsync(VirtualKeyCode.SHIFT) ? 1 : 0);
        }

        void HandleNumLockKeyPressed(ModifierKeyBase numLockKey)
        {
            _allLogicalKeys.OfType<NumLockSensitiveKey>().ToList().ForEach(x => x.SelectedIndex = numLockKey.IsInEffect ? 1 : 0);
        }
    }
}