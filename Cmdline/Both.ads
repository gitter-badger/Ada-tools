with Ada.Text_IO, Spec;
use Ada.Text_IO, Spec;

package Both is
--@version 1.0

	type TestInt is range 0 .. 1;

	type TestMod is mod 8;
	
	procedure Hello_World;

end Both;