   M S B A M L   `   `   `  ����E  APOP3client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:POP3client
POP3client  5   	   9!clr-namespace:POP3client.ControlsPOP3client.Controls  5   	       POP3client.Timer   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:POP3client     5   	   6Controls!clr-namespace:POP3client.Controls     5   	     � Title$  定时发送��5	   	   $	��300��6   $	��600��6#   .��5
      � ���	#FFD7FDC6 � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      .+�5   
   �� $	��时��6   $��Center=�6   $��254,80,306,146q�5      $��2��6%   $��44��69    J� RenderTransformOrigin$ 2.455,0.558&�6E   �� 5   
   $	��分��6   $��Center=�6   $��442,80,116,146q�5      $��2��6%   $��44��69   *   $POP3client.Controls.WaterMarkTextBox  5   
   -   5          Name$ iHour��5   �   $��2��6$   $��32��68   $��140,80,372,158q�6D    �� TextWrapping$
 Wrap=�6\   $�� ��6p   $��Center=�6x   
�������6�     5   
   -   5        $ iMinute��5   �   $��2��6$   $��32��68   $��338,80,174,158q�6D   $
 Wrap=�6\   $�� ��6p   $��Center=�6x   
�������6�   %   POP3client.Controls.ImageButton  5   
   -   5          Name$ 
Send_Later��5   /   %   POP3client.Controls.ImageButton   Click$ SendButton_Click��5      $��1��5      $��发送��6C      Icon$ Icons/Get.png��5         
ButtonType$ IconWithTextVertical=�5   &   $��226,136,212,44q�6H   $ -0.01,1.081&�6`   ���	#FFE3F5DB6�   5      5      