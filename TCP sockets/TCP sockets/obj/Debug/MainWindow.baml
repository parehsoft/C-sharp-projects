   M S B A M L   `   `   `  ����F  BTCP sockets, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TCP_socketsTCP_sockets  5   	       TCP_sockets.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TCP_sockets     5   	   -   5          � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ tb_messages��5
      $
��Left=�6'   $	��145��6B    �� TextWrapping$
 Wrap=�6O   $j��dPut the listening port, broadcasting port and server's IP address in, than hit start button above...��6c   $	��Top=�6�   $	��507��6�    �� AcceptsReturn .  �� 
IsReadOnly . $��0,27,0,0q�6  �� 5   
   -   5         ��Name$ 	btn_start��5      $"��---------- Start! ----------��6$   $
��Left=�6K   $	��Top=�6f   $	��507��6~   $��0,5,0,0q�6�   �� 5   
   -   5        $ tb_listeningPort��5      $
��Left=�6,   $��23��6G   $
 Wrap=�6S   $��Listening port.��6g   $	��Top=�6~   $	��120��6�   $��	0,177,0,0q�6�   �� 5   
   -   5        $ tb_broadcastingPort��5      $
��Left=�6/   $��23��6J   $��	0,205,0,0q�6V   $
 Wrap=�6i   $��Broadcasting port.��6}   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ btn_send��5      $��SEND!!!!��6#   $
��Left=�66   $��125,178,0,0q�6Q   $	��Top=�6f   $	��382��6~   $��78��6�   6�   �� 5   
   -   5        $ tb_serversIPAddress��5      $
��Left=�6/   $��23��6J   $��	0,233,0,0q�6V   $
 Wrap=�6i   $��Server's IP address.��6}   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ tb_textToSend��5      $
��Left=�6)   $��49��6D   $��	0,261,0,0q�6P   $
 Wrap=�6c   $*��$There should be your text in here...��6w   $	��Top=�6�   $	��507��6�    . 6�   5      5      