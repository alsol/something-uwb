listing = dir('D:/waveforms/maximuses_all/*.txt');
listing = {listing.name};
scatter = zeros(50,50);

for folder = 1:length(listing)
    file = fullfile('D:/waveforms/maximuses_all/', listing{folder});  
    input = fopen(file,'r');
    formatSpec = '%f %f %f\n';
    data = fscanf(input, formatSpec,[3 Inf]);
    fclose(input);
    
    for i = 1:length(data(1,:))-1
        scatter(data(1,i)+1,data(2,i)+1) = data(3,i+1);
    end;
end;

surf(scatter);
xlabel('i');
ylabel('j');