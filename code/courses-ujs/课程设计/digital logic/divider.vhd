library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_unsigned.all;
use ieee.std_logic_arith.all;
entity divider is
generic(n:integer :=10);
port (clk:in std_logic;
q:out std_logic);
end divider;
architecture behave of divider is
signal count :integer range n-1 downto 0:=n-1;
begin
process(clk)
begin
if (clk'event and clk='1' and clk'last_value ='0') then
count<=count+1;
if (count>n/2) then q<='0';
else
q<='1';
end if;
if count<=0 then count<=n-1; 
end if;
end if;
end process;
end behave;