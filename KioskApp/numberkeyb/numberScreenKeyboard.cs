using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;



namespace LoginModule.numberkeyb
{

    public class numberScreenKeyboard : Window
    {
        #region Property & Variable & Constructor
        private static double _WidthTouchKeyboard = 830;

        public static double WidthTouchKeyboard
        {
            get { return _WidthTouchKeyboard; }
            set { _WidthTouchKeyboard = value; }

        }
        private static bool _ShiftFlag;

        protected static bool ShiftFlag
        {
            get { return _ShiftFlag; }
            set { _ShiftFlag = value; }
        }

        private static bool _CapsLockFlag;

        protected static bool CapsLockFlag
        {
            get { return numberScreenKeyboard._CapsLockFlag; }
            set { numberScreenKeyboard._CapsLockFlag = value; }
        }

        private static Window _InstanceObject;

        private static Brush _PreviousTextBoxBackgroundBrush = null;
        private static Brush _PreviousTextBoxBorderBrush = null;
        private static Thickness _PreviousTextBoxBorderThickness;

        private static Control _CurrentControl;
        public static string TouchScreenText
        {
            get
            {
                if (_CurrentControl is TextBox)
                {
                    return ((TextBox)_CurrentControl).Text;
                }
                else if (_CurrentControl is PasswordBox)
                {
                    return ((PasswordBox)_CurrentControl).Password;
                }
                else return "";


            }
            set
            {
                if (_CurrentControl is TextBox)
                {
                    ((TextBox)_CurrentControl).Text = value;
                }
                else if (_CurrentControl is PasswordBox)
                {
                    ((PasswordBox)_CurrentControl).Password = value;
                }


            }

        }
        public static RoutedUICommand CmdTlide = new RoutedUICommand();
        public static RoutedUICommand Cmd1 = new RoutedUICommand();
        public static RoutedUICommand Cmd2 = new RoutedUICommand();
        public static RoutedUICommand Cmd3 = new RoutedUICommand();
        public static RoutedUICommand Cmd4 = new RoutedUICommand();
        public static RoutedUICommand Cmd5 = new RoutedUICommand();
        public static RoutedUICommand Cmd6 = new RoutedUICommand();
        public static RoutedUICommand Cmd7 = new RoutedUICommand();
        public static RoutedUICommand Cmd8 = new RoutedUICommand();
        public static RoutedUICommand Cmd9 = new RoutedUICommand();
        public static RoutedUICommand Cmd0 = new RoutedUICommand();
        public static RoutedUICommand CmdMinus = new RoutedUICommand();
        public static RoutedUICommand CmdPlus = new RoutedUICommand();
        public static RoutedUICommand CmdBackspace = new RoutedUICommand();
        public static RoutedUICommand CmdClear = new RoutedUICommand();

        public numberScreenKeyboard()
        {
            this.Width = WidthTouchKeyboard;

        }

        static numberScreenKeyboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(numberScreenKeyboard), new FrameworkPropertyMetadata(typeof(numberScreenKeyboard)));

          //  SetCommandBinding();
        }
        #endregion
        #region CommandRelatedCode
        //private static void SetCommandBinding()
        //{
        //    CommandBinding CbTlide = new CommandBinding(CmdTlide, RunCommand);
        //    CommandBinding Cb1 = new CommandBinding(Cmd1, RunCommand);
        //    CommandBinding Cb2 = new CommandBinding(Cmd2, RunCommand);
        //    CommandBinding Cb3 = new CommandBinding(Cmd3, RunCommand);
        //    CommandBinding Cb4 = new CommandBinding(Cmd4, RunCommand);
        //    CommandBinding Cb5 = new CommandBinding(Cmd5, RunCommand);
        //    CommandBinding Cb6 = new CommandBinding(Cmd6, RunCommand);
        //    CommandBinding Cb7 = new CommandBinding(Cmd7, RunCommand);
        //    CommandBinding Cb8 = new CommandBinding(Cmd8, RunCommand);
        //    CommandBinding Cb9 = new CommandBinding(Cmd9, RunCommand);
        //    CommandBinding Cb0 = new CommandBinding(Cmd0, RunCommand);
        //    CommandBinding CbMinus = new CommandBinding(CmdMinus, RunCommand);
        //    CommandBinding CbPlus = new CommandBinding(CmdPlus, RunCommand);
        //    CommandBinding CbBackspace = new CommandBinding(CmdBackspace, RunCommand);

        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbTlide);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb1);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb2);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb3);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb4);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb5);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb6);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb7);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb8);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb9);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cb0);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbMinus);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbPlus);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbBackspace);


        //    CommandBinding CbTab = new CommandBinding(CmdTab, RunCommand);
        //    CommandBinding CbQ = new CommandBinding(CmdQ, RunCommand);
        //    CommandBinding Cbw = new CommandBinding(Cmdw, RunCommand);
        //    CommandBinding CbE = new CommandBinding(CmdE, RunCommand);
        //    CommandBinding CbR = new CommandBinding(CmdR, RunCommand);
        //    CommandBinding CbT = new CommandBinding(CmdT, RunCommand);
        //    CommandBinding CbY = new CommandBinding(CmdY, RunCommand);
        //    CommandBinding CbU = new CommandBinding(CmdU, RunCommand);
        //    CommandBinding CbI = new CommandBinding(CmdI, RunCommand);
        //    CommandBinding Cbo = new CommandBinding(CmdO, RunCommand);
        //    CommandBinding CbP = new CommandBinding(CmdP, RunCommand);
        //    CommandBinding CbOpenCrulyBrace = new CommandBinding(CmdOpenCrulyBrace, RunCommand);
        //    CommandBinding CbEndCrultBrace = new CommandBinding(CmdEndCrultBrace, RunCommand);
        //    CommandBinding CbOR = new CommandBinding(CmdOR, RunCommand);

        //    CommandBinding CbCapsLock = new CommandBinding(CmdCapsLock, RunCommand);
        //    CommandBinding CbA = new CommandBinding(CmdA, RunCommand);
        //    CommandBinding CbS = new CommandBinding(CmdS, RunCommand);
        //    CommandBinding CbD = new CommandBinding(CmdD, RunCommand);
        //    CommandBinding CbF = new CommandBinding(CmdF, RunCommand);
        //    CommandBinding CbG = new CommandBinding(CmdG, RunCommand);
        //    CommandBinding CbH = new CommandBinding(CmdH, RunCommand);
        //    CommandBinding CbJ = new CommandBinding(CmdJ, RunCommand);
        //    CommandBinding CbK = new CommandBinding(CmdK, RunCommand);
        //    CommandBinding CbL = new CommandBinding(CmdL, RunCommand);
        //    CommandBinding CbColon = new CommandBinding(CmdColon, RunCommand);
        //    CommandBinding CbDoubleInvertedComma = new CommandBinding(CmdDoubleInvertedComma, RunCommand);
        //    CommandBinding CbEnter = new CommandBinding(CmdEnter, RunCommand);

        //    CommandBinding CbShift = new CommandBinding(CmdShift, RunCommand);
        //    CommandBinding CbZ = new CommandBinding(CmdZ, RunCommand);
        //    CommandBinding CbX = new CommandBinding(CmdX, RunCommand);
        //    CommandBinding CbC = new CommandBinding(CmdC, RunCommand);
        //    CommandBinding CbV = new CommandBinding(CmdV, RunCommand);
        //    CommandBinding CbB = new CommandBinding(CmdB, RunCommand);
        //    CommandBinding CbN = new CommandBinding(CmdN, RunCommand);
        //    CommandBinding CbM = new CommandBinding(CmdM, RunCommand);
        //    CommandBinding CbGreaterThan = new CommandBinding(CmdGreaterThan, RunCommand);
        //    CommandBinding CbLessThan = new CommandBinding(CmdLessThan, RunCommand);
        //    CommandBinding CbQuestion = new CommandBinding(CmdQuestion, RunCommand);



        //    CommandBinding CbSpaceBar = new CommandBinding(CmdSpaceBar, RunCommand);
        //    CommandBinding CbClear = new CommandBinding(CmdClear, RunCommand);

        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbTab);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbQ);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cbw);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbE);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbR);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbT);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbY);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbU);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbI);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), Cbo);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbP);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbOpenCrulyBrace);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbEndCrultBrace);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbOR);

        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbCapsLock);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbA);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbS);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbD);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbF);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbG);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbH);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbJ);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbK);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbL);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbColon);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbDoubleInvertedComma);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbEnter);

        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbShift);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbZ);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbX);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbC);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbV);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbB);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbN);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbM);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbGreaterThan);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbLessThan);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbQuestion);



        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbSpaceBar);
        //    CommandManager.RegisterClassCommandBinding(typeof(numberScreenKeyboard), CbClear);

        //}
        static void RunCommand(object sender, ExecutedRoutedEventArgs e)
        {

            if (e.Command == CmdTlide)  //First Row
            {


            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += "`";
            //    }
            //    else
            //    {
            //        numberScreenKeyboard.TouchScreenText += "~";
            //        ShiftFlag = false;
            //    }
            }
            else if (e.Command == Cmd1)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "1";
                }
                else
                {
                    numberScreenKeyboard.TouchScreenText += "!";
                    ShiftFlag = false;
                }

            }
            else if (e.Command == Cmd2)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "2";
                }
                else
                {
                    numberScreenKeyboard.TouchScreenText += "@";
                    ShiftFlag = false;
                }

            }
            else if (e.Command == Cmd3)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "3";
                }
                else
                {
                    numberScreenKeyboard.TouchScreenText += "#";
                    ShiftFlag = false;
                }

            }
            else if (e.Command == Cmd4)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "4";
                }
                else
                {
                    numberScreenKeyboard.TouchScreenText += "$";
                    ShiftFlag = false;
                }

            }
            else if (e.Command == Cmd5)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "5";
                }
               
               
            }
            else if (e.Command == Cmd6)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "6";
                }
               

            }
            else if (e.Command == Cmd7)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "7";
                }
              

            }
            else if (e.Command == Cmd8)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "8";
                }
               
            }
            else if (e.Command == Cmd9)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "9";
                }
             
            }
            else if (e.Command == Cmd0)
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += "0";
                }
               
            }
            //else if (e.Command == CmdMinus)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += "-";
            //    }
                

            //}
            //else if (e.Command == CmdPlus)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += "=";
            //    }
               
            //}
            else if (e.Command == CmdBackspace)
            {
                if (!string.IsNullOrEmpty(numberScreenKeyboard.TouchScreenText))
                {
                    numberScreenKeyboard.TouchScreenText = numberScreenKeyboard.TouchScreenText.Substring(0, numberScreenKeyboard.TouchScreenText.Length - 1);
                }

            }
            //else if (e.Command == CmdTab)  //Second Row
            //{
            //    numberScreenKeyboard.TouchScreenText += "     ";
            //}
            //else if (e.Command == CmdQ)
            //{
            //    AddKeyBoardINput('Q');
            //}
            //else if (e.Command == Cmdw)
            //{
            //    AddKeyBoardINput('w');
            //}
            //else if (e.Command == CmdE)
            //{
            //    AddKeyBoardINput('E');
            //}
            //else if (e.Command == CmdR)
            //{
            //    AddKeyBoardINput('R');
            //}
            //else if (e.Command == CmdT)
            //{
            //    AddKeyBoardINput('T');
            //}
            //else if (e.Command == CmdY)
            //{
            //    AddKeyBoardINput('Y');
            //}
            //else if (e.Command == CmdU)
            //{
            //    AddKeyBoardINput('U');

            //}
            //else if (e.Command == CmdI)
            //{
            //    AddKeyBoardINput('I');
            //}
            //else if (e.Command == CmdO)
            //{
            //    AddKeyBoardINput('O');
            //}
            //else if (e.Command == CmdP)
            //{
            //    AddKeyBoardINput('P');
            //}
            //else if (e.Command == CmdOpenCrulyBrace)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += "[";
            //    }
            //    else
            //    {
            //        numberScreenKeyboard.TouchScreenText += "{";
            //        ShiftFlag = false;
            //    }
            //}
            //else if (e.Command == CmdEndCrultBrace)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += "]";
            //    }
            //    else
            //    {
            //        numberScreenKeyboard.TouchScreenText += "}";
            //        ShiftFlag = false;
            //    }
            //}
            //else if (e.Command == CmdOR)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += @"\";
            //    }
            //    else
            //    {
            //        numberScreenKeyboard.TouchScreenText += "|";
            //        ShiftFlag = false;
            //    }
            //}
            //else if (e.Command == CmdCapsLock)  ///Third ROw
            //{

            //    if (!CapsLockFlag)
            //    {
            //        CapsLockFlag = true;
            //    }
            //    else
            //    {
            //        CapsLockFlag = false;

            //    }
            //}
            //else if (e.Command == CmdA)
            //{
            //    AddKeyBoardINput('A');
            //}
            //else if (e.Command == CmdS)
            //{
            //    AddKeyBoardINput('S');
            //}
            //else if (e.Command == CmdD)
            //{
            //    AddKeyBoardINput('D');
            //}
            //else if (e.Command == CmdF)
            //{
            //    AddKeyBoardINput('F');
            //}
            //else if (e.Command == CmdG)
            //{
            //    AddKeyBoardINput('G');
            //}
            //else if (e.Command == CmdH)
            //{
            //    AddKeyBoardINput('H');
            //}
            //else if (e.Command == CmdJ)
            //{
            //    AddKeyBoardINput('J');
            //}
            //else if (e.Command == CmdK)
            //{
            //    AddKeyBoardINput('K');
            //}
            //else if (e.Command == CmdL)
            //{
            //    AddKeyBoardINput('L');

            //}
            //else if (e.Command == CmdColon)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += ";";
            //    }
            //    else
            //    {
            //        numberScreenKeyboard.TouchScreenText += ":";
            //        ShiftFlag = false;
            //    }

            //}
            //else if (e.Command == CmdDoubleInvertedComma)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += "'";
            //    }
            //    else
            //    {
            //        numberScreenKeyboard.TouchScreenText += Char.ConvertFromUtf32(34);
            //        ShiftFlag = false;
            //    }


         //   }
            //else if (e.Command == CmdEnter)
            //{
            //    if (_InstanceObject != null)
            //    {
            //        _InstanceObject.Close();
            //        _InstanceObject = null;
            //    }
            //    _CurrentControl.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));


            }
            //else if (e.Command == CmdShift) //Fourth Row
            //{

            //    ShiftFlag = true; ;


            //}
            //else if (e.Command == CmdZ)
            //{
            //    AddKeyBoardINput('Z');

            //}
            //else if (e.Command == CmdX)
            //{
            //    AddKeyBoardINput('X');

            //}
            //else if (e.Command == CmdC)
            //{
            //    AddKeyBoardINput('C');

            //}
            //else if (e.Command == CmdV)
            //{
            //    AddKeyBoardINput('V');

            //}
            //else if (e.Command == CmdB)
            //{
            //    AddKeyBoardINput('B');

            //}
            //else if (e.Command == CmdN)
            //{
            //    AddKeyBoardINput('N');

            //}
            //else if (e.Command == CmdM)
            //{
            //    AddKeyBoardINput('M');

            //}
            //else if (e.Command == CmdLessThan)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += ",";
            //    }
            //    else
            //    {
            //        numberScreenKeyboard.TouchScreenText += "<";
            //        ShiftFlag = false;
            //    }

            //}
            //else if (e.Command == CmdGreaterThan)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += ".";
            //    }
            //    else
            //    {
            //        numberScreenKeyboard.TouchScreenText += ">";
            //        ShiftFlag = false;
            //    }

            //}
            //else if (e.Command == CmdQuestion)
            //{
            //    if (!ShiftFlag)
            //    {
            //        numberScreenKeyboard.TouchScreenText += "/";
            //    }
            //    else
            //    {
            //        numberScreenKeyboard.TouchScreenText += "?";
            //        ShiftFlag = false;
            //    }

            //}
            //else if (e.Command == CmdSpaceBar)//Last row
            //{

            //    numberScreenKeyboard.TouchScreenText += " ";
            //}
            //else if (e.Command == CmdClear)//Last row
            //{

            //    numberScreenKeyboard.TouchScreenText = "";
            //}
        
        #endregion
        #region Main Functionality
        private static void AddKeyBoardINput(char input)
        {
            if (CapsLockFlag)
            {
                if (ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += char.ToLower(input).ToString();
                    ShiftFlag = false;

                }
                else
                {
                    numberScreenKeyboard.TouchScreenText += char.ToUpper(input).ToString();
                }
            }
            else
            {
                if (!ShiftFlag)
                {
                    numberScreenKeyboard.TouchScreenText += char.ToLower(input).ToString();
                }
                else
                {
                    numberScreenKeyboard.TouchScreenText += char.ToUpper(input).ToString();
                    ShiftFlag = false;
                }
            }
        }


        private static void syncchild()
        {
            if (_CurrentControl != null && _InstanceObject != null)
            {

                Point virtualpoint = new Point(0, _CurrentControl.ActualHeight + 3);
                Point Actualpoint = _CurrentControl.PointToScreen(virtualpoint);

                /*  if (WidthTouchKeyboard + Actualpoint.X > SystemParameters.VirtualScreenWidth)
                  {
                      double difference = WidthTouchKeyboard + Actualpoint.X - SystemParameters.VirtualScreenWidth;
                      _InstanceObject.Left = Actualpoint.X - difference;
                  }
                  else if (!(Actualpoint.X > 1))
                  {
                      _InstanceObject.Left = 1;
                  }
                  else
                      _InstanceObject.Left = Actualpoint.X;*/
                _InstanceObject.Top = Actualpoint.Y;
                _InstanceObject.Show();
            }


        }

        public static bool GetnumberScreenKeyboard(DependencyObject obj)
        {
            return (bool)obj.GetValue(numberScreenKeyboardProperty);
        }

        public static void SetnumberScreenKeyboard(DependencyObject obj, bool value)
        {
            obj.SetValue(numberScreenKeyboardProperty, value);
        }

        public static readonly DependencyProperty numberScreenKeyboardProperty =
            DependencyProperty.RegisterAttached("numberScreenKeyboard", typeof(bool), typeof(numberScreenKeyboard), new UIPropertyMetadata(default(bool), numberScreenKeyboardPropertyChanged));



        static void numberScreenKeyboardPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement host = sender as FrameworkElement;
            if (host != null)
            {
                host.GotFocus += new RoutedEventHandler(OnGotFocus);
                host.LostFocus += new RoutedEventHandler(OnLostFocus);
            }

        }



        static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            Control host = sender as Control;

            _PreviousTextBoxBackgroundBrush = host.Background;
            _PreviousTextBoxBorderBrush = host.BorderBrush;
            _PreviousTextBoxBorderThickness = host.BorderThickness;

            //host.Background = Brushes.White;
            host.BorderBrush = Brushes.Black;
            host.BorderThickness = new Thickness(4);


            _CurrentControl = host;

            if (_InstanceObject == null)
            {
                FrameworkElement ct = host;
                while (true)
                {
                    if (ct is Window)
                    {
                        ((Window)ct).LocationChanged += new EventHandler(numberScreenKeyboard_LocationChanged);
                        ((Window)ct).Activated += new EventHandler(numberScreenKeyboard_Activated);
                        ((Window)ct).Deactivated += new EventHandler(numberScreenKeyboard_Deactivated);
                        break;
                    }
                    ct = (FrameworkElement)ct.Parent;
                }

                _InstanceObject = new numberScreenKeyboard();
                _InstanceObject.AllowsTransparency = true;
                _InstanceObject.WindowStyle = WindowStyle.None;
                _InstanceObject.ShowInTaskbar = false;
                _InstanceObject.ShowInTaskbar = false;
                _InstanceObject.Topmost = true;

                host.LayoutUpdated += new EventHandler(tb_LayoutUpdated);
            }



        }

        static void numberScreenKeyboard_Deactivated(object sender, EventArgs e)
        {
            if (_InstanceObject != null)
            {
                _InstanceObject.Topmost = false;
            }
        }

        static void numberScreenKeyboard_Activated(object sender, EventArgs e)
        {
            if (_InstanceObject != null)
            {
                _InstanceObject.Topmost = true;
            }
        }



        static void numberScreenKeyboard_LocationChanged(object sender, EventArgs e)
        {
            syncchild();
        }

        static void tb_LayoutUpdated(object sender, EventArgs e)
        {
            syncchild();
        }



        static void OnLostFocus(object sender, RoutedEventArgs e)
        {

            Control host = sender as Control;
            host.Background = _PreviousTextBoxBackgroundBrush;
            host.BorderBrush = _PreviousTextBoxBorderBrush;
            host.BorderThickness = _PreviousTextBoxBorderThickness;

            if (_InstanceObject != null)
            {
                _InstanceObject.Close();
                _InstanceObject = null;
            }



        }

        #endregion
    }
}
