   M S B A M L   `   `   `  ����@  <Salon, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Salon.WindowsSalon.Windows  5          Salon.Windows.AddCustomer   5   
   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Salon.Windows     5        � WindowStartupLocation$  CenterScreen=�5   
    � Title$' !Изменение клиента��5       � Icon$ /Resource/logo1.jpg��6!   $	��310��6<   $	��490��6H   .��5	      � ���	#ffdadada.+�5
   
   �� $	��200��6   $��20q�6    
�������$��Center=�6?    �� Padding$ 30q�6Z   $	��305��6g    �� CornerRadius$ 20��6v   $	��410��6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 30Q�6#    
  shadow   Color# [  63      ShadowDepth$ 0Q�6S   ._�5      �� $	��-20q�6   .+�5      �� $��ФИО:��6   $��	20 10 0 0q�6(   $
��Left=�6;   $��Monotype Corsiva)�6V   $��16(�6t   �� 5      -   5        	 ��Name$	 
FIOTextBox��5      $��30��6.   $��Monotype Corsiva)�6:   $��14(�6X   $��	20 5 35 5q�6f   6y   �� 5      $��Телефон:��6   $��	20 10 0 0q�6,   $
��Left=�6?   $��Monotype Corsiva)�6Z   $��16(�6x   �� 5      -   5        $	 PhoneTextBox��5      $��30��60   $��Monotype Corsiva)�6<   $��14(�6Z   $��	20 5 35 5q�6h   6{   �� 5      $��
Адрес��6   $��	20 10 0 0q�6)   $
��Left=�6<   $��Monotype Corsiva)�6W   $��16(�6u   �� 5      -   5        $	 AddressTextBox��5      $��30��62   $��Monotype Corsiva)�6>   $��14(�6\   $��	20 5 35 5q�6j   �� 5      $��Логин:��6   $��	20 10 0 0q�6*   $
��Left=�6=   $��Monotype Corsiva)�6X   $��16(�6v   �� 5      -   5        $	 LoginTextBox��5      $��30��60   $��Monotype Corsiva)�6<   $��14(�6Z   $��	20 5 35 5q�6h   6{   �� 5      $��Пароль:��6   $��	20 10 0 0q�6+   $
��Left=�6>   $��Monotype Corsiva)�6Y   $��16(�6w   G� 5      -   5        
 G�Name$
 PasswordTextBox��5      $��30��67   $��Monotype Corsiva)�6C   $��14(�6a   $��
20 5 35 20q�6o   6�   �� 5      -   5         ��Name$ 	AddButton��5      $	��175��6-   $��30��69   $��Center=�6E   $'��!Сохранить клиента��6c   6�   5      5      5      5      