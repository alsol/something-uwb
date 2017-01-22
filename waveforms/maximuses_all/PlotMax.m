listing = dir('D:/waveforms/maximuses_all/*.txt');
listing = {listing.name};

data = [];
for folder = 1:length(listing)
    file = fullfile('D:/waveforms/maximuses_all/', listing{folder});  
    input = fopen(file,'r');
    formatSpec = '%f %f %f\n';
    data = [data fscanf(input, formatSpec,[3 Inf])];
    fclose(input);
end;

x_o = data(1,:);
y_o = data(2,:);
z_o = data(3,:);

%% Fit.
[xData, yData, zData] = prepareSurfaceData( x_o, y_o, z_o );

% Set up fittype and options.
ft = 'linearinterp';

% Fit model to data.
[fitresult, gof] = fit( [xData, yData], zData, ft, 'Normalize', 'on' );

% Plot fit with data.
figure( 'Name', 'untitled fit 1' );
h = plot( fitresult, [xData, yData], zData );
legend( h, 'untitled fit 1', 'z_o vs. x_o, y_o', 'Location', 'NorthEast' );
% Label axes
xlabel x_o
ylabel y_o
zlabel z_o
grid on
