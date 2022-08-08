## Расширение /root раздела на виртуальной centos 7 налету без перезагрузки. Разметка дисков при установке автоматическая.


- Расширяем виртуальный диск на хосте виртуализации
- Переопрашиваем его внутри гостевой системы 
`echo 1 > /sys/class/block/sda/device/rescan`
- Исправляем Gpt (например через parted)
`echo "Fix\nFix\n\n\n | parted ---pretend-input-tty /dev/sda print"`
- Пересоздаем раздел с lvm расширяя его на все свободное место <pre><code>
(    
&emsp; echo d # Удаляем старый раздел с lvm
&emsp; echo 3 # Он 3й
&emsp; echo n # Добовляем новый раздел
&emsp; echo 3 # Так же с номером 3
&emsp; echo   # Задаем первый сектор  
&emsp; echo   # Задаем последний сектор
&emsp; echo t # Меняем тип
&emsp; echo E6D6D379-F507-44C2-A23C-238F2A3DF928 # На lvm
&emsp; echo w # Записываем изменения
) | sudo fdisk /dev/sda
</code></pre>
- Перечитываем таблицу разделов
`partprobe /dev/sda`
- Расширяем physical volume до максимума
`pvresize /dev/sda3`
- Расширяем logical volume, используя максимум доступного места:
`lvresize -r -l+100%FREE /dev/centos_ansible-client/root`
- Изменяем размер файловой системы
`xfs_growfs /`