   M S B A M L   `   `   `  ����E  APOP3client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:POP3client
POP3client  5   	   9!clr-namespace:POP3client.ControlsPOP3client.Controls  5   	       POP3client.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:POP3client     5   	   6controls!clr-namespace:POP3client.Controls     5   	     � Title$  Login��5	   	   $	��400��6   $	��500��6$    � 
ResizeMode$ NoResize=�60    � WindowStartupLocation$ CenterScreen=�5
   	    � UseLayoutRounding . .��5      �� ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63    �� Opacity$	 0.3Q�6F   .��5      �  � Color$ 	#FFD4903E��6    � Offset$ 0Q�61   � 5      $ 	#FF00A2FF��6   $ 1Q�61   � 5      $ 	#FF659B65��6   $ 0.526Q�61   5      ._�5   
   � 	 � RowDefinitions	 5      �� 5      $	��24* �6    �� 5      $	��16* �6    �� 5      $	��16* �6    �� 5      $	��16* �6    �� 5      $	��16* �6    .+�5      �� $��0��6   $��96��6"   $��96��6.   ._�5      �� $��.\icons\Logo.png��6   $
��Fill=�62   
 J� Effect
 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$
 Gray��6'      Opacity$	 0.5Q�64      
BlurRadius$ 10Q�5   '      ShadowDepth$ 5Q�67      	Direction$	 270Q�5       5"      �� 5#      $��1��6   ._�5$      �� $��MailManager��6   $��36(�6/   $��	UltraBold"�6=   $��Italic$�6T   $��Center=�5%      $��Center=�65   
����� �5&      
 5'        5(      $
 Gray��6'   $	 0.5Q�64   $ 10Q�5)   '   $ 5Q�67   $	 270Q�5*      5,      �� 5-      $��2��6   ._�5.      *   $POP3client.Controls.WaterMarkTextBox  -   5          Name$ AcconutTextBox��5.   ,   *   $POP3client.Controls.WaterMarkTextBox   TextChanged$  AcconutTextBox_TextChanged��50   d      	WaterMark$ 账号��5.   D   $��36��5/   $   $	��300��60   $��Center=�50   $   $��Center=�6?   $�� ��6\   6d   51      �� 52      $��3��6   ._�53        -   5        $ PasswordTextBox��53   ,   $ 密码��6E   $��36��54   $   $	��300��60   $��Center=�55   $   $��Center=�6?   $�� ��6\   56      � 57      $��4��6    � ColumnDefinitions 58      �� 59      �� 5:      .+�5<      �� $��0��6   ._�5=      %   POP3client.Controls.ImageButton  -   5          Name$ LoginButton��5=   +   %   POP3client.Controls.ImageButton   Click$ LoginButton_Click��5@   #   $��36��5=   @   $��36��6L      Icon$ .\icons\Login.png��5>   #   $	��Top=�5?   #   $��Right=�6;   $��0,0,30,0q�6W      
ButtonType$
 Icon=�5@   =   
 5A        5B      $
 Gray��6+   $	 0.5Q�68   $ 10Q�5C   '   $ 5Q�67   $	 270Q�5D      5F      �� 5G      $��1��6   ._�5H        -   5        $ CloseButton��5H   +   $ CloseButton_Click��5K   #   $��36��5H   @   $��36��6L   $ .\icons\Close.png��5I   #   $	��Top=�5J   #   $
��Left=�6;   $��30,0,0,0q�6V   $
 Icon=�5K   =   
 5L        5M      $
 Gray��6+   $	 0.5Q�68   $ 10Q�5N   '   $ 5Q�67   $	 270Q�5O      5Q      5S      5T      5U      5W      