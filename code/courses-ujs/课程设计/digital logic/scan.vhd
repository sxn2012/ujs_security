library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;

entity scan is
  port(  clk,en:in std_logic;
        input0,input1,input2,input3:in std_logic_vector(7 downto 0);
       ledag:out std_logic_vector(6 downto 0);
         sel:out std_logic_vector(2 downto 0));
end scan;

architecture behave of scan is
  signal out1:std_logic_vector(3 downto 0);
  signal sel1:std_logic_vector(2 downto 0);
  
begin
 
 p1:process(clk)
   begin 
     if rising_edge(clk) then 
        sel1<=sel1+1;
     end if;
     sel<=sel1;
 end process p1;
   
 
 p2:process(sel1,input0,input1,input2,input3,en)
 variable a:std_logic:='0';
    begin
    if en='0' then a:=not a;
	else a:=a;
	end if;
	if a='1' then
      case sel1 is
        when "111"=>out1<=input0(3 downto 0);
        when "110"=>out1<=input0(7 downto 4);
        when "101"=>out1<=input1(3 downto 0);
        when "100"=>out1<=input1(7 downto 4);
		when "011"=>out1<=input2(3 downto 0);
        when "010"=>out1<=input2(7 downto 4);
		when "001"=>out1<=input3(3 downto 0);
        when "000"=>out1<=input3(7 downto 4);     
        when others=>out1<="1111";
      end case;
     else out1<="1111";
	end if;
 end process p2;
  
 
 p3:process(out1)
 
   begin 
      case out1 is
        when  "0000"=>ledag<="0111111";
        when  "0001"=>ledag<="0000110";
        when  "0010"=>ledag<="1011011";
        when  "0011"=>ledag<="1001111";
        when  "0100"=>ledag<="1100110";
        when  "0101"=>ledag<="1101101";
        when  "0110"=>ledag<="1111101";
        when  "0111"=>ledag<="0000111";
        when  "1000"=>ledag<="1111111";
		when  "1001"=>ledag<="1101111";	
		
			
	    when others=>ledag<="0000000";
	  end case;	 
	   
 end process p3;	
 

	
 end behave;