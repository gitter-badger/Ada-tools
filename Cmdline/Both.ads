with Ada.Text_IO, Spec;
use Ada.Text_IO, Spec;

package Both is

	type TestInt is range 0 .. 1;

	type TestMod is mod 8;
	
	procedure Hello_World;

end Both;