   M S B A M L   `   `   `  ����X  TAssignment6AirlineReservation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Assignment6AirlineReservationAssignment6AirlineReservation  5   	   3    -Assignment6AirlineReservation.wndAddPassenger   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:Assignment6AirlineReservation     5   	     � Title$  Add Passenger��5   	   $��220.253��6   $	��300��60    � WindowStartupLocation$ CenterScreen=�6<   .��5	      � ���	#FFB8B8B8 � ColumnDefinitions 5
   
   �� 5      $	��51* �6   �� 5      $	��95* �6    � RowDefinitions 5   
   �� 5      $	��41* �6   �� 5      $	��48* �6   �� 5      $	��50* �6   �� 5      $	��50* �6   .+�5   
   �� -   5         ��Name$ lblTitle��5      $'��!Please Enter The Passenger's Name��6"   $��Center=�6N   $
��48,0q�6k   $��Bottom=�6y   $��2��6�   $��26��6�   $	��196��6�   �� 5   
   -   5        $ label��5      $��
First Name��6   $��Right=�64   $��	0,11,5,11q�6P   $��1��6c   $��Center=�6p   $��26��6�   $��67��6�   �� 5   
   -   5         ��Name$ txtFirstName��5      $��1��6(   $��Center=�68   $��22��6U   $��34,13,36,13q�6a   $��1��6v    �� TextWrapping$
 Wrap=�6�   $��Center=�6�   $	��120��6�    �� 	MaxLength$ 10��6�    �� MaxLines$ 1��6�   6�   �� 5   
   -   5        $ 
label_Copy��5      $��	Last Name��6$   $��Right=�68   $��	0,12,5,12q�6T   $��2��6g   $��Center=�6t   $��26��6�   $��66��6�   �� 5   
   -   5        $ txtLastName��5      $��1��6'   $��Center=�67   $��22��6T   $��34,14,36,14q�6`   $��2��6u   $
 Wrap=�6�   $��Center=�6�   $	��120��6�   $ 10��6�   $ 1��6�   6�   �� 5   
   -   5        	 ��Name$	 	cmdCancel��5      $��Cancel��6$   $��1��65   $��Right=�6E   $��0,22,49.6,0q�6a   $��3��6v   $	��Top=�6�   $��75��6�   
 �� IsCancel
 . 6�   �� 5   
   -   5        $	 cmdSave��5      $
��Save��6"   $
��Left=�61   $��	50,22,0,0q�6L   $��3��6_   $	��Top=�6l   $��75��6�   $��2��6�   6�   �� 5   
   -   5        $ error��5      $�� ��6   $
��Left=�6*   $��77,43.4,0,0q�6E   $��2��6Z   $	��Top=�6g   $��2��6   $��24��6�   $��2��6�   $	��167��6�   5      5      