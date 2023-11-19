package com.example.fallguys.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import org.springframework.http.HttpStatus;

@Getter
@AllArgsConstructor
public class SuccessResponseDto {
    private HttpStatus status;
}
