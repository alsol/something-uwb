input = fopen('D:/waveforms/maximuses/output.txt','r');
formatSpec = '%f %f %f\n';
data = fscanf(input, formatSpec,[3 Inf]);
X = data(1,:);
Y = data(2,:);

scatter = zeros(100,100);

for i = 1:length(data(1,:))-1
    scatter(data(1,i)+1,data(2,i)+1) = data(3,i+1);
end;

surf(scatter);
xlabel('i');
ylabel('j');