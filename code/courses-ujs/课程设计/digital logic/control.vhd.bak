library ieee;
 use ieee.std_logic_1164.all;
 use ieee.std_logic_unsigned.all;
 use ieee.std_logic_arith.all;
 
 entity control is
	port(buttom,reset:in std_logic;
		move,zero:out std_logic);
end control;

architecture behave of control is
signal result:std_logic_vector(1 downto 0);
begin
	process(buttom,reset)
		begin
			if (reset='0') then result<="01";
			else 
				if (buttom='0') then
					result(1)<=not result(1);
					result(0)<='0';
					end if;
				end if;
					end process;
	move<=result(1);
	zero<=result(0);
end behave;
		
					