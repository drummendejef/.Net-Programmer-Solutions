   M S B A M L   `   `   `  ����B  >NQueens, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:NQueensNQueens  5   	       NQueens.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:NQueens     5   	   X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  
MainWindow��5	   	   $	��350��6   $	��525��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ 	textBlock��5      $
��Left=�6'   $��	10,10,0,0q�6B   $
��Wrap=�6U   $��Aantal koniginnen:��6i   $	��Top=�6�   $��16��6�   $��99��6�   %  Xceed.Wpf.Toolkit.IntegerUpDown  5   
   -   5          Name$ iudAantalQueens��5      �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 4X�66      Minimum$ 4X�6@   $��
114,10,0,0q�6M   $��22��6a   $	��Top=�6m   $
��Left=�6�   $��69��6�   �� 5   
   -   5         ��Name$ btnStart��5      $��Start��6#   $��
188,11,0,4q�63   $
��Left=�6^   $��75��6y   $	��Top=�6�     NQueens.Bord  5   
   -   5          Name$ 
canvasBord��5      $��10q�6)   $��1��65   �� 5   
   -   5        $ btnAsync��5      $��Async��6#   $
��Left=�63   $��
268,11,0,0q�6N   $	��Top=�6b   $��75��6z   6�   5      5      