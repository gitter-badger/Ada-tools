with Ada.Text_IO, Spec;
use Ada.Text_IO, Spec;

package Both with Ada_2012 is
--@description Just a simple test package
--@version 1.0

	type TestInt is range 0 .. 1;

	type TestMod is mod 8;

	type TestFloat is digits 8;

	type TestFloatRange is digits 8 range 0.0 .. 12.0;

	type TestFixed is delta 0.01 range 0.0 .. 8.0;

	type TestDecimal is delta 0.1 digits 8;

	type TestDecimalRange is delta 0.1 digits 8 range 0.0 .. 10.0;

end Both;