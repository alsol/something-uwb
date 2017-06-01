function [fitresult, gof] = createFit(x_o, y_o, z_o)
%CREATEFIT(X_O,Y_O,Z_O)
%  Create a fit.
%
%  Data for 'untitled fit 1' fit:
%      X Input : x_o
%      Y Input : y_o
%      Z Output: z_o
%  Output:
%      fitresult : a fit object representing the fit.
%      gof : structure with goodness-of fit info.
%
%  See also FIT, CFIT, SFIT.

%  Auto-generated by MATLAB on 24-Jan-2017 00:05:57


%% Fit: 'untitled fit 1'.
[xData, yData, zData] = prepareSurfaceData( x_o, y_o, z_o );

% Set up fittype and options.
ft = fittype( 'a + x*b + y*c', 'independent', {'x', 'y'}, 'dependent', 'z' );
opts = fitoptions( 'Method', 'NonlinearLeastSquares' );
opts.Display = 'Off';
opts.StartPoint = [0.54747561876263 0.00902726312478774 0.387512309088257];

% Fit model to data.
[fitresult, gof] = fit( [xData, yData], zData, ft, opts );

% Plot fit with data.
figure( 'Name', 'untitled fit 1' );
h = plot( fitresult, [xData, yData], zData );
legend( h, 'untitled fit 1', 'z_o vs. x_o, y_o', 'Location', 'NorthEast' );
% Label axes
xlabel x_o
ylabel y_o
zlabel z_o
grid on
view( 129.7, 42.8 );


