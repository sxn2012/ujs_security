library ieee;
 use ieee.std_logic_1164.all;
 use ieee.std_logic_unsigned.all;
 use ieee.std_logic_arith.all;
 
 entity cnt60 is
 port(move,zero,clk:in std_logic;
	co:out std_logic;
	qout:out std_logic_vector(7 downto 0));
end cnt60;
architecture behave of cnt60 is 
signal qh,ql:std_logic_vector(3 downto 0);
begin
	process(clk,move,zero)
	begin
		if (zero='1') then qout<="00000000"; co<='0'; qh<="0000"; ql<="0000";
		elsif (move='1') then
		if (clk'event and clk='1') then
			if (ql=9) then   
                 if (qh=5) then qh<="0000";ql<="0000";co<='1';
                 else qh<=qh+1;ql<="0000";co<='0';
                 end if;
               else ql<=ql+1;qh<=qh; co<='0';      
               end if;
    
		end if;
qout<=qh&ql;
	end if;
	end process;
end behave;
	