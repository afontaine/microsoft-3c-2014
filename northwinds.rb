require 'net/http'
def get_value(filter)
	url = URI::Parser.new.escape("http://services.odata.org/Northwind/Northwind.svc/Products/$count?$filter=" + filter)
	uri = URI(url)
	Net::HTTP.get(uri)
end


File.open("northwinds_in.txt") do |i|
	File.open("northwinds_out.txt", 'w') do |o|
		i.each_line do |l|
			o.write(get_value(l).to_s)
			o.write("\n")
		end
	end
end