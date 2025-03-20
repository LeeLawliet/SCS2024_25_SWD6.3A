# Installing and Importing Packages
install.packages("factoextra")
install.packages("ggplot2")
install.packages("readxl")
library(readxl)
library(factoextra)
library(ggplot2)

# Importing data from Excel, remove 'Class' (y) attribute
data <- read_excel("C:/Users/deadr/Documents/MCAST/SCS2024_25_SWD6.3A/HomeAssignment/Dataset/DryBeanDataset/Dry_Bean_Dataset.xlsx")
data$Class <- NULL

# PCA
pca_result <- prcomp(data, center = TRUE, scale. = TRUE)
summary(pca_result)

# Eigenvalues
eigenvalues <- pca_result$sdev^2
sorted_eigenvalues <- sort(eigenvalues, decreasing = TRUE)
sorted_eigenvalues

# Scree plot
fviz_eig(pca_result)
fviz_eig(pca_result, addlabels = TRUE, ylim = c(0, 100))

# Select top 3 principal components
pca_data <- data.frame(pca_result$x[, 1:2])

# Saving updated data
write.csv(pca_data, "C:/Users/deadr/Documents/MCAST/SCS2024_25_SWD6.3A/HomeAssignment/updated_data.csv", row.names = FALSE)

# Display the first few rows of the updated data
head(pca_data, 10)
