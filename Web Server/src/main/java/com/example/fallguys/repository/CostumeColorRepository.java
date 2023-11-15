package com.example.fallguys.repository;

import com.example.fallguys.domain.CostumeColor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface CostumeColorRepository extends JpaRepository<CostumeColor, Long> {

}
