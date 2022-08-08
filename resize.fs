

echo 1 > /sys/class/block/sda/device/rescan

parted
print
Fix
Fix
quit

fdisk /dev/sda
 d 
 3
 n 
 3
 t
 E6D6D379-F507-44C2-A23C-238F2A3DF928
 w
partprobe /dev/sda
pvresize -v /dev/sda3
lvresize -r -l+100%FREE /dev/centos_ansible-client/root