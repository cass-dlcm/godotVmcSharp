/*
    godotVmcSharp
    Copyright (C) 2023  Cassandra de la Cruz-Munoz

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as published
    by the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
    */

using godotOscSharp;

namespace godotVmcSharp
{
    public class InvalidArgumentType
    {
        public static string GetErrorString(string address, string name, char expected, char actual)
        {
            return $"Invalid argument type for argument \"{name}\" of \"{address}\". Expected {expected}, received {actual}.";
        }
        public static string GetErrorString(godotOscSharp.Address address, string name, char expected, char actual)
        {
            return GetErrorString(address.ToString(), name, expected, actual);
        }
    }
}